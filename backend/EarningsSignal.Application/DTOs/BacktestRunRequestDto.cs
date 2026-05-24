namespace EarningsSignal.Application.DTOs;

public record BacktestRunRequestDto(
    string StrategyType,
    int HoldingDays,
    DateOnly? FromDate,
    DateOnly? ToDate,
    decimal MinReactionPct);
