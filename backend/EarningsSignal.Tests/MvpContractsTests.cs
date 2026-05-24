using EarningsSignal.Application.DTOs;
using EarningsSignal.Domain.Entities;

namespace EarningsSignal.Tests;

public class MvpContractsTests
{
    [Fact]
    public void UpcomingEarningsDto_ExposesWeeklyScannerFields()
    {
        var dto = new UpcomingEarningsDto(
            Ticker: "MSFT",
            CompanyName: "Microsoft Corporation",
            ReportDate: new DateOnly(2026, 05, 27),
            ReportTime: "After Close",
            Sector: "Technology",
            ExpectationPressureScore: 64.5m,
            PreSignal: "Volatility Watch");

        Assert.Equal("MSFT", dto.Ticker);
        Assert.Equal("Microsoft Corporation", dto.CompanyName);
        Assert.Equal(new DateOnly(2026, 05, 27), dto.ReportDate);
        Assert.Equal("After Close", dto.ReportTime);
        Assert.Equal("Technology", dto.Sector);
        Assert.Equal(64.5m, dto.ExpectationPressureScore);
        Assert.Equal("Volatility Watch", dto.PreSignal);
    }

    [Fact]
    public void LiveSignalDto_ExposesLiveSignalsFields()
    {
        var generatedAtUtc = DateTime.UtcNow;

        var dto = new LiveSignalDto(
            Ticker: "NVDA",
            CompanyName: "NVIDIA Corporation",
            Sector: "Technology",
            SignalType: "Strong Short",
            Score: 87.3m,
            ReasonSummary: "Priced-for-perfection setup with bearish confirmation.",
            GeneratedAtUtc: generatedAtUtc);

        Assert.Equal("NVDA", dto.Ticker);
        Assert.Equal("NVIDIA Corporation", dto.CompanyName);
        Assert.Equal("Technology", dto.Sector);
        Assert.Equal("Strong Short", dto.SignalType);
        Assert.Equal(87.3m, dto.Score);
        Assert.Equal("Priced-for-perfection setup with bearish confirmation.", dto.ReasonSummary);
        Assert.Equal(generatedAtUtc, dto.GeneratedAtUtc);
    }

    [Fact]
    public void Company_CollectionsAreInitialized()
    {
        var company = new Company();

        Assert.NotNull(company.EarningsEvents);
        Assert.NotNull(company.DailyPrices);
        Assert.NotNull(company.Signals);
    }
}
