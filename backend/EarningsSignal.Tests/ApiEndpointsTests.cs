using System.Net.Http.Json;
using EarningsSignal.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EarningsSignal.Tests;

public class ApiEndpointsTests(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetCompanies_ReturnsFiveSeededCompanies()
    {
        var companies = await _client.GetFromJsonAsync<List<CompanyResponse>>("/api/companies");

        Assert.NotNull(companies);
        var materializedCompanies = companies!;
        Assert.Equal(5, materializedCompanies.Count);
        Assert.Contains(materializedCompanies, company => company.Ticker == "AAPL");
        Assert.Contains(materializedCompanies, company => company.Ticker == "MSFT");
        Assert.Contains(materializedCompanies, company => company.Ticker == "AMZN");
        Assert.Contains(materializedCompanies, company => company.Ticker == "JPM");
        Assert.Contains(materializedCompanies, company => company.Ticker == "NVDA");
    }

    [Fact]
    public async Task GetUpcomingEarnings_ReturnsFiveUpcomingEvents()
    {
        var upcoming = await _client.GetFromJsonAsync<List<UpcomingEarningsResponse>>("/api/earnings/upcoming");

        Assert.NotNull(upcoming);
        var materializedUpcoming = upcoming!;
        Assert.Equal(5, materializedUpcoming.Count);
        Assert.All(materializedUpcoming, item => Assert.True(item.ReportDate >= DateOnly.FromDateTime(DateTime.UtcNow.Date)));
    }

    [Fact]
    public async Task GetLiveSignals_ReturnsThreeLiveSignals()
    {
        var liveSignals = await _client.GetFromJsonAsync<List<LiveSignalResponse>>("/api/signals/live");

        Assert.NotNull(liveSignals);
        var materializedLiveSignals = liveSignals!;
        Assert.Equal(3, materializedLiveSignals.Count);
        Assert.All(materializedLiveSignals, signal => Assert.False(string.IsNullOrWhiteSpace(signal.Ticker)));
        Assert.All(materializedLiveSignals, signal => Assert.False(string.IsNullOrWhiteSpace(signal.SignalType)));
        Assert.True(materializedLiveSignals[0].GeneratedAtUtc >= materializedLiveSignals[1].GeneratedAtUtc);
        Assert.True(materializedLiveSignals[1].GeneratedAtUtc >= materializedLiveSignals[2].GeneratedAtUtc);
    }

    public sealed record CompanyResponse(
        Guid Id,
        string Ticker,
        string Name,
        string Sector,
        string Industry);

    public sealed record UpcomingEarningsResponse(
        string Ticker,
        string CompanyName,
        DateOnly ReportDate,
        string ReportTime,
        string Sector,
        decimal ExpectationPressureScore,
        string PreSignal);

    public sealed record LiveSignalResponse(
        string Ticker,
        string CompanyName,
        string Sector,
        string SignalType,
        decimal Score,
        string ReasonSummary,
        DateTime GeneratedAtUtc);
}

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"earnings-signal-tests-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<EarningsSignalDbContext>));
            services.RemoveAll(typeof(EarningsSignalDbContext));

            services.AddDbContext<EarningsSignalDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));
        });
    }
}
