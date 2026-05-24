using EarningsSignal.Application.DTOs;
using EarningsSignal.Infrastructure.Data;
using EarningsSignal.Infrastructure.Seed;
using EarningsSignal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Tests;

public class BacktestServiceTests
{
    [Fact]
    public async Task RunBacktest_WithSeededData_ReturnsRunAndTrades()
    {
        await using var dbContext = await CreateSeededContextAsync();
        var service = new DbBacktestService(dbContext);

        var result = await service.RunBacktestAsync(
            new BacktestRunRequestDto(
                StrategyType: "CleanMissShort",
                HoldingDays: 3,
                FromDate: null,
                ToDate: null,
                MinReactionPct: -2m));

        Assert.True(result.Run.TotalEventsEvaluated >= 5);
        Assert.True(result.Run.TotalTrades >= 1);
        Assert.All(result.Trades, trade => Assert.Equal("Short", trade.Direction));

        var runs = await service.GetBacktestRunsAsync();
        Assert.Contains(runs, run => run.Id == result.Run.Id);

        var runById = await service.GetBacktestRunAsync(result.Run.Id);
        Assert.NotNull(runById);
        Assert.Equal(result.Run.Id, runById!.Id);

        var tradesByRun = await service.GetBacktestTradesAsync(result.Run.Id);
        Assert.Equal(result.Trades.Count, tradesByRun.Count);
    }

    [Fact]
    public async Task RunBacktest_WithUnsupportedStrategy_ThrowsArgumentException()
    {
        await using var dbContext = await CreateSeededContextAsync();
        var service = new DbBacktestService(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => service.RunBacktestAsync(
            new BacktestRunRequestDto(
                StrategyType: "NotARealStrategy",
                HoldingDays: 3,
                FromDate: null,
                ToDate: null,
                MinReactionPct: -2m)));
    }

    [Fact]
    public async Task RunBacktest_WithInvalidHoldingDays_ThrowsArgumentException()
    {
        await using var dbContext = await CreateSeededContextAsync();
        var service = new DbBacktestService(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => service.RunBacktestAsync(
            new BacktestRunRequestDto(
                StrategyType: "CleanMissShort",
                HoldingDays: 0,
                FromDate: null,
                ToDate: null,
                MinReactionPct: -2m)));
    }

    [Fact]
    public async Task RunBacktest_WithFromDateAfterToDate_ThrowsArgumentException()
    {
        await using var dbContext = await CreateSeededContextAsync();
        var service = new DbBacktestService(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() => service.RunBacktestAsync(
            new BacktestRunRequestDto(
                StrategyType: "CleanMissShort",
                HoldingDays: 3,
                FromDate: new DateOnly(2026, 05, 20),
                ToDate: new DateOnly(2026, 05, 01),
                MinReactionPct: -2m)));
    }

    private static async Task<EarningsSignalDbContext> CreateSeededContextAsync()
    {
        var options = new DbContextOptionsBuilder<EarningsSignalDbContext>()
            .UseInMemoryDatabase($"earnings-signal-tests-{Guid.NewGuid()}")
            .Options;

        var dbContext = new EarningsSignalDbContext(options);
        await DatabaseSeeder.SeedAsync(dbContext);
        return dbContext;
    }
}
