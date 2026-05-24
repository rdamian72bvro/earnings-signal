using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Seed;

namespace EarningsSignal.Infrastructure.Services;

public class MockCompanyService : ICompanyService
{
    public Task<IReadOnlyList<CompanyDto>> GetCompaniesAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<CompanyDto> companies =
            MockSeedData.Companies
                .Select(company => new CompanyDto(
                    company.Id,
                    company.Ticker,
                    company.Name,
                    company.Sector,
                    company.Industry))
                .ToList();

        return Task.FromResult(companies);
    }
}
