namespace EarningsSignal.Application.DTOs;

public record LiveSignalDto(
    string Ticker,
    string CompanyName,
    string Sector,
    string SignalType,
    decimal Score,
    string ReasonSummary,
    DateTime GeneratedAtUtc);
