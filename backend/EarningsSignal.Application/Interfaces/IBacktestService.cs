using EarningsSignal.Application.DTOs;

namespace EarningsSignal.Application.Interfaces;

public interface IBacktestService
{
    Task<IReadOnlyList<BacktestRunDto>> GetBacktestRunsAsync(CancellationToken cancellationToken = default);
    Task<BacktestRunDto?> GetBacktestRunAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<BacktestTradeDto>> GetBacktestTradesAsync(Guid backtestRunId, CancellationToken cancellationToken = default);
    Task<BacktestRunResultDto> RunBacktestAsync(BacktestRunRequestDto request, CancellationToken cancellationToken = default);
}
