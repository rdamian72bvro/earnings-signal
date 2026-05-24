using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Services;

public class DbCompanyService(EarningsSignalDbContext dbContext) : ICompanyService
{
    public async Task<IReadOnlyList<CompanyDto>> GetCompaniesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Companies
            .AsNoTracking()
            .OrderBy(company => company.Ticker)
            .Select(company => new CompanyDto(
                company.Id,
                company.Ticker,
                company.Name,
                company.Sector,
                company.Industry))
            .ToListAsync(cancellationToken);
    }
}
