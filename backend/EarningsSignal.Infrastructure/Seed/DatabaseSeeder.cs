using EarningsSignal.Domain.Entities;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Seed;

public static class DatabaseSeeder
{
    private static readonly Guid AppleCompanyId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid MicrosoftCompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid AmazonCompanyId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid JpmCompanyId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid NvidiaCompanyId = Guid.Parse("55555555-5555-5555-5555-555555555555");

    private static readonly Guid AppleEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1");
    private static readonly Guid MicrosoftEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2");
    private static readonly Guid AmazonEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3");
    private static readonly Guid JpmEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4");
    private static readonly Guid NvidiaEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5");

    private static readonly Guid AppleHistoryEventId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1");
    private static readonly Guid MicrosoftHistoryEventId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc2");
    private static readonly Guid AmazonHistoryEventId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc3");
    private static readonly Guid JpmHistoryEventId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc4");
    private static readonly Guid NvidiaHistoryEventId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc5");

    public static async Task SeedAsync(EarningsSignalDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var now = DateTime.UtcNow;

        await UpsertCompaniesAsync(dbContext, cancellationToken);

        var upcomingEvents = BuildUpcomingEvents(today);
        var historicalEvents = BuildHistoricalEvents(today);

        await UpsertEarningsEventsAsync(
            dbContext,
            upcomingEvents.Concat(historicalEvents).ToArray(),
            cancellationToken);

        await UpsertEarningsEstimatesAsync(dbContext, BuildHistoricalEstimates(historicalEvents), cancellationToken);
        await UpsertEarningsActualsAsync(dbContext, BuildHistoricalActuals(historicalEvents), cancellationToken);
        await UpsertDailyPricesAsync(dbContext, BuildHistoricalDailyPrices(historicalEvents), cancellationToken);
        await UpsertLiveSignalsAsync(dbContext, now, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task UpsertCompaniesAsync(
        EarningsSignalDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var companySeeds = new[]
        {
            new Company
            {
                Id = AppleCompanyId,
                Ticker = "AAPL",
                Name = "Apple Inc.",
                Sector = "Technology",
                Industry = "Consumer Electronics"
            },
            new Company
            {
                Id = MicrosoftCompanyId,
                Ticker = "MSFT",
                Name = "Microsoft Corporation",
                Sector = "Technology",
                Industry = "Software"
            },
            new Company
            {
                Id = AmazonCompanyId,
                Ticker = "AMZN",
                Name = "Amazon.com, Inc.",
                Sector = "Consumer Discretionary",
                Industry = "Internet Retail"
            },
            new Company
            {
                Id = JpmCompanyId,
                Ticker = "JPM",
                Name = "JPMorgan Chase & Co.",
                Sector = "Financials",
                Industry = "Banks"
            },
            new Company
            {
                Id = NvidiaCompanyId,
                Ticker = "NVDA",
                Name = "NVIDIA Corporation",
                Sector = "Technology",
                Industry = "Semiconductors"
            }
        };

        var companyIds = companySeeds.Select(seed => seed.Id).ToArray();

        var existingCompanies = await dbContext.Companies
            .Where(company => companyIds.Contains(company.Id))
            .ToDictionaryAsync(company => company.Id, cancellationToken);

        foreach (var seed in companySeeds)
        {
            if (existingCompanies.TryGetValue(seed.Id, out var company))
            {
                company.Ticker = seed.Ticker;
                company.Name = seed.Name;
                company.Sector = seed.Sector;
                company.Industry = seed.Industry;
            }
            else
            {
                dbContext.Companies.Add(seed);
            }
        }
    }

    private static async Task UpsertEarningsEventsAsync(
        EarningsSignalDbContext dbContext,
        IReadOnlyList<EarningsEvent> earningsEventSeeds,
        CancellationToken cancellationToken)
    {
        var earningsEventIds = earningsEventSeeds.Select(seed => seed.Id).ToArray();

        var existingEarningsEvents = await dbContext.EarningsEvents
            .Where(earningsEvent => earningsEventIds.Contains(earningsEvent.Id))
            .ToDictionaryAsync(earningsEvent => earningsEvent.Id, cancellationToken);

        foreach (var seed in earningsEventSeeds)
        {
            if (existingEarningsEvents.TryGetValue(seed.Id, out var earningsEvent))
            {
                earningsEvent.CompanyId = seed.CompanyId;
                earningsEvent.ReportDate = seed.ReportDate;
                earningsEvent.ReportTime = seed.ReportTime;
                earningsEvent.ExpectationPressureScore = seed.ExpectationPressureScore;
                earningsEvent.PreSignal = seed.PreSignal;
            }
            else
            {
                dbContext.EarningsEvents.Add(seed);
            }
        }
    }

    private static async Task UpsertEarningsEstimatesAsync(
        EarningsSignalDbContext dbContext,
        IReadOnlyList<EarningsEstimate> estimateSeeds,
        CancellationToken cancellationToken)
    {
        var estimateIds = estimateSeeds.Select(seed => seed.Id).ToArray();

        var existingEstimates = await dbContext.EarningsEstimates
            .Where(estimate => estimateIds.Contains(estimate.Id))
            .ToDictionaryAsync(estimate => estimate.Id, cancellationToken);

        foreach (var seed in estimateSeeds)
        {
            if (existingEstimates.TryGetValue(seed.Id, out var estimate))
            {
                estimate.EarningsEventId = seed.EarningsEventId;
                estimate.EpsEstimate = seed.EpsEstimate;
                estimate.RevenueEstimate = seed.RevenueEstimate;
                estimate.AsOfUtc = seed.AsOfUtc;
            }
            else
            {
                dbContext.EarningsEstimates.Add(seed);
            }
        }
    }

    private static async Task UpsertEarningsActualsAsync(
        EarningsSignalDbContext dbContext,
        IReadOnlyList<EarningsActual> actualSeeds,
        CancellationToken cancellationToken)
    {
        var actualIds = actualSeeds.Select(seed => seed.Id).ToArray();

        var existingActuals = await dbContext.EarningsActuals
            .Where(actual => actualIds.Contains(actual.Id))
            .ToDictionaryAsync(actual => actual.Id, cancellationToken);

        foreach (var seed in actualSeeds)
        {
            if (existingActuals.TryGetValue(seed.Id, out var actual))
            {
                actual.EarningsEventId = seed.EarningsEventId;
                actual.EpsActual = seed.EpsActual;
                actual.RevenueActual = seed.RevenueActual;
                actual.ReportedAtUtc = seed.ReportedAtUtc;
            }
            else
            {
                dbContext.EarningsActuals.Add(seed);
            }
        }
    }

    private static async Task UpsertDailyPricesAsync(
        EarningsSignalDbContext dbContext,
        IReadOnlyList<DailyPrice> priceSeeds,
        CancellationToken cancellationToken)
    {
        var priceIds = priceSeeds.Select(seed => seed.Id).ToArray();

        var existingPrices = await dbContext.DailyPrices
            .Where(price => priceIds.Contains(price.Id))
            .ToDictionaryAsync(price => price.Id, cancellationToken);

        foreach (var seed in priceSeeds)
        {
            if (existingPrices.TryGetValue(seed.Id, out var price))
            {
                price.CompanyId = seed.CompanyId;
                price.TradeDate = seed.TradeDate;
                price.Open = seed.Open;
                price.High = seed.High;
                price.Low = seed.Low;
                price.Close = seed.Close;
                price.Volume = seed.Volume;
            }
            else
            {
                dbContext.DailyPrices.Add(seed);
            }
        }
    }

    private static async Task UpsertLiveSignalsAsync(
        EarningsSignalDbContext dbContext,
        DateTime now,
        CancellationToken cancellationToken)
    {
        var liveSignalSeeds = new[]
        {
            new Signal
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                CompanyId = AppleCompanyId,
                EarningsEventId = AppleEventId,
                SignalType = "Short",
                Score = 79.5m,
                ReasonSummary = "High expectation pressure and weak post-report price reaction.",
                GeneratedAtUtc = now.AddMinutes(-40),
                IsLive = true
            },
            new Signal
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                CompanyId = AmazonCompanyId,
                EarningsEventId = AmazonEventId,
                SignalType = "Long Watch",
                Score = 66.0m,
                ReasonSummary = "Mixed quality beat with constructive sector momentum.",
                GeneratedAtUtc = now.AddMinutes(-25),
                IsLive = true
            },
            new Signal
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                CompanyId = NvidiaCompanyId,
                EarningsEventId = NvidiaEventId,
                SignalType = "Strong Short",
                Score = 87.3m,
                ReasonSummary = "Priced-for-perfection setup with bearish confirmation.",
                GeneratedAtUtc = now.AddMinutes(-10),
                IsLive = true
            }
        };

        var signalIds = liveSignalSeeds.Select(seed => seed.Id).ToArray();

        var existingSignals = await dbContext.Signals
            .Where(signal => signalIds.Contains(signal.Id))
            .ToDictionaryAsync(signal => signal.Id, cancellationToken);

        foreach (var seed in liveSignalSeeds)
        {
            if (existingSignals.TryGetValue(seed.Id, out var signal))
            {
                signal.CompanyId = seed.CompanyId;
                signal.EarningsEventId = seed.EarningsEventId;
                signal.SignalType = seed.SignalType;
                signal.Score = seed.Score;
                signal.ReasonSummary = seed.ReasonSummary;
                signal.GeneratedAtUtc = seed.GeneratedAtUtc;
                signal.IsLive = true;
            }
            else
            {
                dbContext.Signals.Add(seed);
            }
        }
    }

    private static IReadOnlyList<EarningsEvent> BuildUpcomingEvents(DateOnly today)
    {
        return
        [
            new EarningsEvent
            {
                Id = AppleEventId,
                CompanyId = AppleCompanyId,
                ReportDate = today.AddDays(2),
                ReportTime = "After Close",
                ExpectationPressureScore = 78.0m,
                PreSignal = "Short Watch"
            },
            new EarningsEvent
            {
                Id = MicrosoftEventId,
                CompanyId = MicrosoftCompanyId,
                ReportDate = today.AddDays(3),
                ReportTime = "After Close",
                ExpectationPressureScore = 64.5m,
                PreSignal = "Volatility Watch"
            },
            new EarningsEvent
            {
                Id = AmazonEventId,
                CompanyId = AmazonCompanyId,
                ReportDate = today.AddDays(4),
                ReportTime = "After Close",
                ExpectationPressureScore = 70.2m,
                PreSignal = "Short Watch"
            },
            new EarningsEvent
            {
                Id = JpmEventId,
                CompanyId = JpmCompanyId,
                ReportDate = today.AddDays(5),
                ReportTime = "Before Open",
                ExpectationPressureScore = 52.8m,
                PreSignal = "No Trade"
            },
            new EarningsEvent
            {
                Id = NvidiaEventId,
                CompanyId = NvidiaCompanyId,
                ReportDate = today.AddDays(6),
                ReportTime = "After Close",
                ExpectationPressureScore = 83.1m,
                PreSignal = "Short Watch"
            }
        ];
    }

    private static IReadOnlyList<EarningsEvent> BuildHistoricalEvents(DateOnly today)
    {
        var anchorDate = today.AddDays(-40);

        return
        [
            new EarningsEvent
            {
                Id = AppleHistoryEventId,
                CompanyId = AppleCompanyId,
                ReportDate = anchorDate,
                ReportTime = "After Close",
                ExpectationPressureScore = 72.0m,
                PreSignal = "Historical"
            },
            new EarningsEvent
            {
                Id = MicrosoftHistoryEventId,
                CompanyId = MicrosoftCompanyId,
                ReportDate = anchorDate.AddDays(3),
                ReportTime = "After Close",
                ExpectationPressureScore = 67.5m,
                PreSignal = "Historical"
            },
            new EarningsEvent
            {
                Id = AmazonHistoryEventId,
                CompanyId = AmazonCompanyId,
                ReportDate = anchorDate.AddDays(6),
                ReportTime = "After Close",
                ExpectationPressureScore = 74.1m,
                PreSignal = "Historical"
            },
            new EarningsEvent
            {
                Id = JpmHistoryEventId,
                CompanyId = JpmCompanyId,
                ReportDate = anchorDate.AddDays(9),
                ReportTime = "Before Open",
                ExpectationPressureScore = 48.6m,
                PreSignal = "Historical"
            },
            new EarningsEvent
            {
                Id = NvidiaHistoryEventId,
                CompanyId = NvidiaCompanyId,
                ReportDate = anchorDate.AddDays(12),
                ReportTime = "After Close",
                ExpectationPressureScore = 86.4m,
                PreSignal = "Historical"
            }
        ];
    }

    private static IReadOnlyList<EarningsEstimate> BuildHistoricalEstimates(IReadOnlyList<EarningsEvent> historicalEvents)
    {
        var byId = historicalEvents.ToDictionary(earningsEvent => earningsEvent.Id);

        return
        [
            new EarningsEstimate
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd1"),
                EarningsEventId = AppleHistoryEventId,
                EpsEstimate = 2.00m,
                RevenueEstimate = 100.00m,
                AsOfUtc = byId[AppleHistoryEventId].ReportDate.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            },
            new EarningsEstimate
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd2"),
                EarningsEventId = MicrosoftHistoryEventId,
                EpsEstimate = 2.50m,
                RevenueEstimate = 120.00m,
                AsOfUtc = byId[MicrosoftHistoryEventId].ReportDate.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            },
            new EarningsEstimate
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd3"),
                EarningsEventId = AmazonHistoryEventId,
                EpsEstimate = 0.80m,
                RevenueEstimate = 150.00m,
                AsOfUtc = byId[AmazonHistoryEventId].ReportDate.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            },
            new EarningsEstimate
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd4"),
                EarningsEventId = JpmHistoryEventId,
                EpsEstimate = 3.10m,
                RevenueEstimate = 132.00m,
                AsOfUtc = byId[JpmHistoryEventId].ReportDate.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            },
            new EarningsEstimate
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd5"),
                EarningsEventId = NvidiaHistoryEventId,
                EpsEstimate = 4.20m,
                RevenueEstimate = 180.00m,
                AsOfUtc = byId[NvidiaHistoryEventId].ReportDate.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            }
        ];
    }

    private static IReadOnlyList<EarningsActual> BuildHistoricalActuals(IReadOnlyList<EarningsEvent> historicalEvents)
    {
        var byId = historicalEvents.ToDictionary(earningsEvent => earningsEvent.Id);

        return
        [
            new EarningsActual
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee1"),
                EarningsEventId = AppleHistoryEventId,
                EpsActual = 1.60m,
                RevenueActual = 96.00m,
                ReportedAtUtc = byId[AppleHistoryEventId].ReportDate.ToDateTime(new TimeOnly(20, 0), DateTimeKind.Utc)
            },
            new EarningsActual
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee2"),
                EarningsEventId = MicrosoftHistoryEventId,
                EpsActual = 2.70m,
                RevenueActual = 118.00m,
                ReportedAtUtc = byId[MicrosoftHistoryEventId].ReportDate.ToDateTime(new TimeOnly(20, 0), DateTimeKind.Utc)
            },
            new EarningsActual
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee3"),
                EarningsEventId = AmazonHistoryEventId,
                EpsActual = 0.92m,
                RevenueActual = 152.00m,
                ReportedAtUtc = byId[AmazonHistoryEventId].ReportDate.ToDateTime(new TimeOnly(20, 0), DateTimeKind.Utc)
            },
            new EarningsActual
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee4"),
                EarningsEventId = JpmHistoryEventId,
                EpsActual = 3.55m,
                RevenueActual = 138.00m,
                ReportedAtUtc = byId[JpmHistoryEventId].ReportDate.ToDateTime(new TimeOnly(14, 0), DateTimeKind.Utc)
            },
            new EarningsActual
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee5"),
                EarningsEventId = NvidiaHistoryEventId,
                EpsActual = 4.80m,
                RevenueActual = 191.00m,
                ReportedAtUtc = byId[NvidiaHistoryEventId].ReportDate.ToDateTime(new TimeOnly(20, 0), DateTimeKind.Utc)
            }
        ];
    }

    private static IReadOnlyList<DailyPrice> BuildHistoricalDailyPrices(IReadOnlyList<EarningsEvent> historicalEvents)
    {
        var byId = historicalEvents.ToDictionary(earningsEvent => earningsEvent.Id);

        return
        [
            // AAPL
            new DailyPrice
            {
                Id = Guid.Parse("f1111111-1111-1111-1111-111111111111"),
                CompanyId = AppleCompanyId,
                TradeDate = byId[AppleHistoryEventId].ReportDate,
                Open = 188.00m,
                High = 191.20m,
                Low = 186.90m,
                Close = 190.00m,
                Volume = 98000000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f1111111-1111-1111-1111-111111111112"),
                CompanyId = AppleCompanyId,
                TradeDate = byId[AppleHistoryEventId].ReportDate.AddDays(1),
                Open = 183.00m,
                High = 184.10m,
                Low = 179.60m,
                Close = 182.00m,
                Volume = 122000000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f1111111-1111-1111-1111-111111111113"),
                CompanyId = AppleCompanyId,
                TradeDate = byId[AppleHistoryEventId].ReportDate.AddDays(4),
                Open = 176.80m,
                High = 177.50m,
                Low = 173.90m,
                Close = 175.00m,
                Volume = 110000000
            },

            // MSFT
            new DailyPrice
            {
                Id = Guid.Parse("f2222222-2222-2222-2222-222222222221"),
                CompanyId = MicrosoftCompanyId,
                TradeDate = byId[MicrosoftHistoryEventId].ReportDate,
                Open = 406.00m,
                High = 412.20m,
                Low = 404.70m,
                Close = 410.00m,
                Volume = 36000000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f2222222-2222-2222-2222-222222222222"),
                CompanyId = MicrosoftCompanyId,
                TradeDate = byId[MicrosoftHistoryEventId].ReportDate.AddDays(1),
                Open = 398.00m,
                High = 399.40m,
                Low = 394.10m,
                Close = 397.00m,
                Volume = 52500000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f2222222-2222-2222-2222-222222222223"),
                CompanyId = MicrosoftCompanyId,
                TradeDate = byId[MicrosoftHistoryEventId].ReportDate.AddDays(4),
                Open = 392.00m,
                High = 393.70m,
                Low = 387.80m,
                Close = 389.00m,
                Volume = 49200000
            },

            // AMZN
            new DailyPrice
            {
                Id = Guid.Parse("f3333333-3333-3333-3333-333333333331"),
                CompanyId = AmazonCompanyId,
                TradeDate = byId[AmazonHistoryEventId].ReportDate,
                Open = 178.00m,
                High = 181.30m,
                Low = 176.40m,
                Close = 180.00m,
                Volume = 74000000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f3333333-3333-3333-3333-333333333332"),
                CompanyId = AmazonCompanyId,
                TradeDate = byId[AmazonHistoryEventId].ReportDate.AddDays(1),
                Open = 176.00m,
                High = 177.50m,
                Low = 173.20m,
                Close = 175.00m,
                Volume = 93000000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f3333333-3333-3333-3333-333333333333"),
                CompanyId = AmazonCompanyId,
                TradeDate = byId[AmazonHistoryEventId].ReportDate.AddDays(4),
                Open = 171.20m,
                High = 172.00m,
                Low = 167.90m,
                Close = 169.00m,
                Volume = 88400000
            },

            // JPM
            new DailyPrice
            {
                Id = Guid.Parse("f4444444-4444-4444-4444-444444444441"),
                CompanyId = JpmCompanyId,
                TradeDate = byId[JpmHistoryEventId].ReportDate,
                Open = 196.00m,
                High = 202.80m,
                Low = 194.90m,
                Close = 201.00m,
                Volume = 21500000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f4444444-4444-4444-4444-444444444442"),
                CompanyId = JpmCompanyId,
                TradeDate = byId[JpmHistoryEventId].ReportDate.AddDays(1),
                Open = 203.00m,
                High = 205.30m,
                Low = 201.60m,
                Close = 204.00m,
                Volume = 24100000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f4444444-4444-4444-4444-444444444443"),
                CompanyId = JpmCompanyId,
                TradeDate = byId[JpmHistoryEventId].ReportDate.AddDays(4),
                Open = 205.20m,
                High = 207.10m,
                Low = 203.70m,
                Close = 206.00m,
                Volume = 22800000
            },

            // NVDA
            new DailyPrice
            {
                Id = Guid.Parse("f5555555-5555-5555-5555-555555555551"),
                CompanyId = NvidiaCompanyId,
                TradeDate = byId[NvidiaHistoryEventId].ReportDate,
                Open = 892.00m,
                High = 926.00m,
                Low = 887.20m,
                Close = 918.00m,
                Volume = 47500000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f5555555-5555-5555-5555-555555555552"),
                CompanyId = NvidiaCompanyId,
                TradeDate = byId[NvidiaHistoryEventId].ReportDate.AddDays(1),
                Open = 924.00m,
                High = 949.00m,
                Low = 920.30m,
                Close = 941.00m,
                Volume = 52300000
            },
            new DailyPrice
            {
                Id = Guid.Parse("f5555555-5555-5555-5555-555555555553"),
                CompanyId = NvidiaCompanyId,
                TradeDate = byId[NvidiaHistoryEventId].ReportDate.AddDays(4),
                Open = 953.00m,
                High = 972.80m,
                Low = 948.00m,
                Close = 964.00m,
                Volume = 49800000
            }
        ];
    }
}
