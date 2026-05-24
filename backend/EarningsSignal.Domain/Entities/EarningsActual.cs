namespace EarningsSignal.Domain.Entities;

public class EarningsActual
{
    public Guid Id { get; set; }
    public Guid EarningsEventId { get; set; }
    public decimal EpsActual { get; set; }
    public decimal RevenueActual { get; set; }
    public DateTime ReportedAtUtc { get; set; }

    public EarningsEvent? EarningsEvent { get; set; }
}
