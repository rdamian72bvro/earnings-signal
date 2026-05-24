namespace EarningsSignal.Application.DTOs;

public record CompanyDto(
    Guid Id,
    string Ticker,
    string Name,
    string Sector,
    string Industry);
