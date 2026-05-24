namespace EarningsSignal.Application.DTOs;

public record BacktestRunResultDto(
    BacktestRunDto Run,
    IReadOnlyList<BacktestTradeDto> Trades);
