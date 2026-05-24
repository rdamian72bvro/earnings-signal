namespace EarningsSignal.Application.DTOs;

public record UpcomingEarningsDto(
    string Ticker,
    string CompanyName,
    DateOnly ReportDate,
    string ReportTime,
    string Sector,
    decimal ExpectationPressureScore,
    string PreSignal);
