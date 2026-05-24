using EarningsSignal.Application.DTOs;

namespace EarningsSignal.Application.Interfaces;

public interface ISignalService
{
    Task<IReadOnlyList<LiveSignalDto>> GetLiveSignalsAsync(CancellationToken cancellationToken = default);
}
