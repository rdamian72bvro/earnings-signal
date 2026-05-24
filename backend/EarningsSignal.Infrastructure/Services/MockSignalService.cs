using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Seed;

namespace EarningsSignal.Infrastructure.Services;

public class MockSignalService : ISignalService
{
    public Task<IReadOnlyList<LiveSignalDto>> GetLiveSignalsAsync(CancellationToken cancellationToken = default)
    {
        var companyById = MockSeedData.Companies.ToDictionary(company => company.Id);

        IReadOnlyList<LiveSignalDto> liveSignals =
            MockSeedData.LiveSignals
                .Where(signal => signal.IsLive)
                .OrderByDescending(signal => signal.GeneratedAtUtc)
                .Select(signal =>
                {
                    var company = companyById[signal.CompanyId];
                    return new LiveSignalDto(
                        company.Ticker,
                        company.Name,
                        company.Sector,
                        signal.SignalType,
                        signal.Score,
                        signal.ReasonSummary,
                        signal.GeneratedAtUtc);
                })
                .ToList();

        return Task.FromResult(liveSignals);
    }
}
