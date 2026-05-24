namespace EarningsSignal.Domain.Entities;

public class DailyPrice
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public DateOnly TradeDate { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public long Volume { get; set; }

    public Company? Company { get; set; }
}
