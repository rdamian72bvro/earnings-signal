using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Domain.Entities;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Services;

public class DbBacktestService(EarningsSignalDbContext dbContext) : IBacktestService
{
    private static readonly IReadOnlyDictionary<string, (string Label, string Direction)> StrategyMetadata =
        new Dictionary<string, (string Label, string Direction)>(StringComparer.OrdinalIgnoreCase)
        {
            ["cleanmissshort"] = ("CleanMissShort", "Short"),
            ["lowqualitybeatshort"] = ("LowQualityBeatShort", "Short"),
            ["beatrejectedshort"] = ("BeatRejectedShort", "Short"),
            ["beatandraiselong"] = ("BeatAndRaiseLong", "Long")
        };

    public async Task<IReadOnlyList<BacktestRunDto>> GetBacktestRunsAsync(CancellationToken cancellationToken = default)
    {
        var runs = await dbContext.BacktestRuns
            .AsNoTracking()
            .OrderByDescending(run => run.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return runs.Select(MapRun).ToList();
    }

    public async Task<BacktestRunDto?> GetBacktestRunAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var run = await dbContext.BacktestRuns
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        return run is null ? null : MapRun(run);
    }

    public async Task<IReadOnlyList<BacktestTradeDto>> GetBacktestTradesAsync(
        Guid backtestRunId,
        CancellationToken cancellationToken = default)
    {
        var trades = await dbContext.BacktestTrades
            .AsNoTracking()
            .Where(trade => trade.BacktestRunId == backtestRunId)
            .OrderBy(trade => trade.EntryDate)
            .ToListAsync(cancellationToken);

        return trades.Select(MapTrade).ToList();
    }

    public async Task<BacktestRunResultDto> RunBacktestAsync(
        BacktestRunRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var strategy = ResolveStrategy(request.StrategyType);
        var holdingDays = ResolveHoldingDays(request.HoldingDays);
        var minReactionPct = request.MinReactionPct;
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        if (request.FromDate is not null && request.ToDate is not null && request.FromDate > request.ToDate)
        {
            throw new ArgumentException("FromDate cannot be later than ToDate.");
        }

        var query = dbContext.EarningsEvents
            .AsNoTracking()
            .Include(earningsEvent => earningsEvent.Company)
            .Include(earningsEvent => earningsEvent.Estimate)
            .Include(earningsEvent => earningsEvent.Actual)
            .Where(earningsEvent =>
                earningsEvent.ReportDate < today
                && earningsEvent.Company != null
                && earningsEvent.Estimate != null
                && earningsEvent.Actual != null);

        if (request.FromDate is not null)
        {
            query = query.Where(earningsEvent => earningsEvent.ReportDate >= request.FromDate.Value);
        }

        if (request.ToDate is not null)
        {
            query = query.Where(earningsEvent => earningsEvent.ReportDate <= request.ToDate.Value);
        }

        var events = await query
            .OrderBy(earningsEvent => earningsEvent.ReportDate)
            .ToListAsync(cancellationToken);

        var trades = new List<BacktestTrade>();

        if (events.Count > 0)
        {
            var companyIds = events
                .Select(earningsEvent => earningsEvent.CompanyId)
                .Distinct()
                .ToArray();

            var minPriceDate = events.Min(earningsEvent => earningsEvent.ReportDate);
            var maxPriceDate = events.Max(earningsEvent => earningsEvent.ReportDate.AddDays(holdingDays + 1));

            var prices = await dbContext.DailyPrices
                .AsNoTracking()
                .Where(price =>
                    companyIds.Contains(price.CompanyId)
                    && price.TradeDate >= minPriceDate
                    && price.TradeDate <= maxPriceDate)
                .ToListAsync(cancellationToken);

            var priceByKey = prices.ToDictionary(price => (price.CompanyId, price.TradeDate));

            foreach (var earningsEvent in events)
            {
                if (!TryBuildTrade(
                        earningsEvent,
                        priceByKey,
                        strategy,
                        holdingDays,
                        minReactionPct,
                        out var trade))
                {
                    continue;
                }

                trades.Add(trade);
            }
        }

        var runId = Guid.NewGuid();
        var createdAtUtc = DateTime.UtcNow;

        foreach (var trade in trades)
        {
            trade.BacktestRunId = runId;
        }

        var winningTrades = trades.Count(trade => trade.ReturnPct > 0m);
        var totalTrades = trades.Count;
        var averageReturnPct = totalTrades == 0 ? 0m : Math.Round(trades.Average(trade => trade.ReturnPct), 4);
        var winRatePct = totalTrades == 0 ? 0m : Math.Round((winningTrades * 100m) / totalTrades, 4);

        var run = new BacktestRun
        {
            Id = runId,
            StrategyType = strategy.Label,
            HoldingDays = holdingDays,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            TotalEventsEvaluated = events.Count,
            TotalTrades = totalTrades,
            WinningTrades = winningTrades,
            WinRatePct = winRatePct,
            AverageReturnPct = averageReturnPct,
            CreatedAtUtc = createdAtUtc,
            Trades = trades
        };

        dbContext.BacktestRuns.Add(run);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BacktestRunResultDto(
            MapRun(run),
            trades
                .OrderBy(trade => trade.EntryDate)
                .Select(MapTrade)
                .ToList());
    }

    private static bool TryBuildTrade(
        EarningsEvent earningsEvent,
        IReadOnlyDictionary<(Guid CompanyId, DateOnly TradeDate), DailyPrice> priceByKey,
        (string Label, string Direction) strategy,
        int holdingDays,
        decimal minReactionPct,
        out BacktestTrade trade)
    {
        trade = null!;

        var reportDate = earningsEvent.ReportDate;
        var entryDate = reportDate.AddDays(1);
        var exitDate = entryDate.AddDays(holdingDays);

        if (!priceByKey.TryGetValue((earningsEvent.CompanyId, reportDate), out var reportPrice))
        {
            return false;
        }

        if (!priceByKey.TryGetValue((earningsEvent.CompanyId, entryDate), out var entryPrice))
        {
            return false;
        }

        if (!priceByKey.TryGetValue((earningsEvent.CompanyId, exitDate), out var exitPrice))
        {
            return false;
        }

        if (entryPrice.Open <= 0m || reportPrice.Close <= 0m)
        {
            return false;
        }

        var estimate = earningsEvent.Estimate!;
        var actual = earningsEvent.Actual!;
        var epsSurprisePct = CalculateSurprisePct(estimate.EpsEstimate, actual.EpsActual);
        var revenueSurprisePct = CalculateSurprisePct(estimate.RevenueEstimate, actual.RevenueActual);
        var reactionPct = CalculatePctChange(reportPrice.Close, entryPrice.Close);

        if (!MatchesStrategy(strategy.Label, epsSurprisePct, revenueSurprisePct, reactionPct, minReactionPct))
        {
            return false;
        }

        var returnPct = CalculateTradeReturnPct(strategy.Direction, entryPrice.Open, exitPrice.Close);
        var notes = $"EPS {epsSurprisePct:F2}%, Revenue {revenueSurprisePct:F2}%, Reaction {reactionPct:F2}%";

        trade = new BacktestTrade
        {
            Id = Guid.NewGuid(),
            EarningsEventId = earningsEvent.Id,
            CompanyId = earningsEvent.CompanyId,
            Ticker = earningsEvent.Company!.Ticker,
            Direction = strategy.Direction,
            SetupType = strategy.Label,
            EntryDate = entryDate,
            EntryPrice = entryPrice.Open,
            ExitDate = exitDate,
            ExitPrice = exitPrice.Close,
            ReturnPct = Math.Round(returnPct, 4),
            Notes = notes
        };

        return true;
    }

    private static (string Label, string Direction) ResolveStrategy(string? strategyType)
    {
        var normalizedKey = (strategyType ?? string.Empty).Trim().ToLowerInvariant();
        if (normalizedKey.Length == 0)
        {
            normalizedKey = "cleanmissshort";
        }

        if (!StrategyMetadata.TryGetValue(normalizedKey, out var strategy))
        {
            throw new ArgumentException(
                "Unsupported strategy type. Supported values: CleanMissShort, LowQualityBeatShort, BeatRejectedShort, BeatAndRaiseLong.");
        }

        return strategy;
    }

    private static int ResolveHoldingDays(int requestedHoldingDays)
    {
        if (requestedHoldingDays < 1 || requestedHoldingDays > 20)
        {
            throw new ArgumentException("HoldingDays must be between 1 and 20.");
        }

        return requestedHoldingDays;
    }

    private static bool MatchesStrategy(
        string strategyLabel,
        decimal epsSurprisePct,
        decimal revenueSurprisePct,
        decimal reactionPct,
        decimal minReactionPct)
    {
        return strategyLabel switch
        {
            "CleanMissShort" => epsSurprisePct < 0m && revenueSurprisePct <= 0m && reactionPct <= minReactionPct,
            "LowQualityBeatShort" => epsSurprisePct > 0m && revenueSurprisePct < 0m && reactionPct <= minReactionPct,
            "BeatRejectedShort" => epsSurprisePct > 0m && reactionPct <= minReactionPct,
            "BeatAndRaiseLong" => epsSurprisePct > 0m && revenueSurprisePct > 0m && reactionPct >= Math.Abs(minReactionPct),
            _ => false
        };
    }

    private static decimal CalculateSurprisePct(decimal estimate, decimal actual)
    {
        if (estimate == 0m)
        {
            return 0m;
        }

        return ((actual - estimate) / Math.Abs(estimate)) * 100m;
    }

    private static decimal CalculatePctChange(decimal fromValue, decimal toValue)
    {
        if (fromValue == 0m)
        {
            return 0m;
        }

        return ((toValue - fromValue) / fromValue) * 100m;
    }

    private static decimal CalculateTradeReturnPct(string direction, decimal entryPrice, decimal exitPrice)
    {
        if (entryPrice == 0m)
        {
            return 0m;
        }

        var longReturnPct = ((exitPrice - entryPrice) / entryPrice) * 100m;
        return direction.Equals("Short", StringComparison.OrdinalIgnoreCase)
            ? -longReturnPct
            : longReturnPct;
    }

    private static BacktestRunDto MapRun(BacktestRun run)
    {
        return new BacktestRunDto(
            run.Id,
            run.StrategyType,
            run.HoldingDays,
            run.FromDate,
            run.ToDate,
            run.TotalEventsEvaluated,
            run.TotalTrades,
            run.WinningTrades,
            run.WinRatePct,
            run.AverageReturnPct,
            run.CreatedAtUtc);
    }

    private static BacktestTradeDto MapTrade(BacktestTrade trade)
    {
        return new BacktestTradeDto(
            trade.Id,
            trade.BacktestRunId,
            trade.Ticker,
            trade.Direction,
            trade.SetupType,
            trade.EntryDate,
            trade.EntryPrice,
            trade.ExitDate,
            trade.ExitPrice,
            trade.ReturnPct,
            trade.Notes);
    }
}
