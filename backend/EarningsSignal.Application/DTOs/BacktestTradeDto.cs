namespace EarningsSignal.Application.DTOs;

public record BacktestTradeDto(
    Guid Id,
    Guid BacktestRunId,
    string Ticker,
    string Direction,
    string SetupType,
    DateOnly EntryDate,
    decimal EntryPrice,
    DateOnly ExitDate,
    decimal ExitPrice,
    decimal ReturnPct,
    string Notes);
