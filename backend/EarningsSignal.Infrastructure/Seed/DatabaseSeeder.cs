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

    public static async Task SeedAsync(EarningsSignalDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var now = DateTime.UtcNow;

        await UpsertCompaniesAsync(dbContext, cancellationToken);
        await UpsertUpcomingEarningsEventsAsync(dbContext, today, cancellationToken);
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

    private static async Task UpsertUpcomingEarningsEventsAsync(
        EarningsSignalDbContext dbContext,
        DateOnly today,
        CancellationToken cancellationToken)
    {
        var earningsEventSeeds = new[]
        {
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
        };

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
}
