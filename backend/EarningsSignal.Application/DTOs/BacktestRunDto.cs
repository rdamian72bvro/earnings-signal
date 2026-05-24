namespace EarningsSignal.Application.DTOs;

public record BacktestRunDto(
    Guid Id,
    string StrategyType,
    int HoldingDays,
    DateOnly? FromDate,
    DateOnly? ToDate,
    int TotalEventsEvaluated,
    int TotalTrades,
    int WinningTrades,
    decimal WinRatePct,
    decimal AverageReturnPct,
    DateTime CreatedAtUtc);
