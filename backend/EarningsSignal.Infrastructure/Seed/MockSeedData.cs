using EarningsSignal.Domain.Entities;

namespace EarningsSignal.Infrastructure.Seed;

public static class MockSeedData
{
    private static readonly DateOnly BaseDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);

    public static IReadOnlyList<Company> Companies { get; } =
    [
        new Company
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Ticker = "AAPL",
            Name = "Apple Inc.",
            Sector = "Technology",
            Industry = "Consumer Electronics"
        },
        new Company
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Ticker = "MSFT",
            Name = "Microsoft Corporation",
            Sector = "Technology",
            Industry = "Software"
        },
        new Company
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Ticker = "AMZN",
            Name = "Amazon.com, Inc.",
            Sector = "Consumer Discretionary",
            Industry = "Internet Retail"
        },
        new Company
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Ticker = "JPM",
            Name = "JPMorgan Chase & Co.",
            Sector = "Financials",
            Industry = "Banks"
        },
        new Company
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Ticker = "NVDA",
            Name = "NVIDIA Corporation",
            Sector = "Technology",
            Industry = "Semiconductors"
        }
    ];

    public static IReadOnlyList<EarningsEvent> UpcomingEarningsEvents { get; } =
    [
        new EarningsEvent
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
            CompanyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ReportDate = BaseDate.AddDays(2),
            ReportTime = "After Close",
            ExpectationPressureScore = 78.0m,
            PreSignal = "Short Watch"
        },
        new EarningsEvent
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
            CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            ReportDate = BaseDate.AddDays(3),
            ReportTime = "After Close",
            ExpectationPressureScore = 64.5m,
            PreSignal = "Volatility Watch"
        },
        new EarningsEvent
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
            CompanyId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            ReportDate = BaseDate.AddDays(4),
            ReportTime = "After Close",
            ExpectationPressureScore = 70.2m,
            PreSignal = "Short Watch"
        },
        new EarningsEvent
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
            CompanyId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            ReportDate = BaseDate.AddDays(5),
            ReportTime = "Before Open",
            ExpectationPressureScore = 52.8m,
            PreSignal = "No Trade"
        },
        new EarningsEvent
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
            CompanyId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            ReportDate = BaseDate.AddDays(6),
            ReportTime = "After Close",
            ExpectationPressureScore = 83.1m,
            PreSignal = "Short Watch"
        }
    ];

    public static IReadOnlyList<Signal> LiveSignals { get; } =
    [
        new Signal
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
            CompanyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            EarningsEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
            SignalType = "Short",
            Score = 79.5m,
            ReasonSummary = "High expectation pressure and weak post-report price reaction.",
            GeneratedAtUtc = DateTime.UtcNow.AddMinutes(-40),
            IsLive = true
        },
        new Signal
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
            CompanyId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            EarningsEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
            SignalType = "Long Watch",
            Score = 66.0m,
            ReasonSummary = "Mixed quality beat with constructive sector momentum.",
            GeneratedAtUtc = DateTime.UtcNow.AddMinutes(-25),
            IsLive = true
        },
        new Signal
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
            CompanyId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            EarningsEventId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
            SignalType = "Strong Short",
            Score = 87.3m,
            ReasonSummary = "Priced-for-perfection setup with bearish confirmation.",
            GeneratedAtUtc = DateTime.UtcNow.AddMinutes(-10),
            IsLive = true
        }
    ];
}
