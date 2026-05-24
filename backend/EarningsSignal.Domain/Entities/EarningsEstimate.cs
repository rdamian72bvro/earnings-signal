namespace EarningsSignal.Domain.Entities;

public class EarningsEstimate
{
    public Guid Id { get; set; }
    public Guid EarningsEventId { get; set; }
    public decimal EpsEstimate { get; set; }
    public decimal RevenueEstimate { get; set; }
    public DateTime AsOfUtc { get; set; }

    public EarningsEvent? EarningsEvent { get; set; }
}
