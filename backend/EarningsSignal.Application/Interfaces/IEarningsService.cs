using EarningsSignal.Application.DTOs;

namespace EarningsSignal.Application.Interfaces;

public interface IEarningsService
{
    Task<IReadOnlyList<UpcomingEarningsDto>> GetUpcomingEarningsAsync(CancellationToken cancellationToken = default);
}
