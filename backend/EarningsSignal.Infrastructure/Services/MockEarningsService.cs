using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Seed;

namespace EarningsSignal.Infrastructure.Services;

public class MockEarningsService : IEarningsService
{
    public Task<IReadOnlyList<UpcomingEarningsDto>> GetUpcomingEarningsAsync(CancellationToken cancellationToken = default)
    {
        var companyById = MockSeedData.Companies.ToDictionary(company => company.Id);

        IReadOnlyList<UpcomingEarningsDto> upcoming =
            MockSeedData.UpcomingEarningsEvents
                .OrderBy(earningsEvent => earningsEvent.ReportDate)
                .Select(earningsEvent =>
                {
                    var company = companyById[earningsEvent.CompanyId];
                    return new UpcomingEarningsDto(
                        company.Ticker,
                        company.Name,
                        earningsEvent.ReportDate,
                        earningsEvent.ReportTime,
                        company.Sector,
                        earningsEvent.ExpectationPressureScore,
                        earningsEvent.PreSignal);
                })
                .ToList();

        return Task.FromResult(upcoming);
    }
}
