using EarningsSignal.Application.DTOs;

namespace EarningsSignal.Application.Interfaces;

public interface ICompanyService
{
    Task<IReadOnlyList<CompanyDto>> GetCompaniesAsync(CancellationToken cancellationToken = default);
}
