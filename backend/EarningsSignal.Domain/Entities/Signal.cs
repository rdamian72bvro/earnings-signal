namespace EarningsSignal.Domain.Entities;

public class Signal
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? EarningsEventId { get; set; }
    public string SignalType { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public string ReasonSummary { get; set; } = string.Empty;
    public DateTime GeneratedAtUtc { get; set; }
    public bool IsLive { get; set; }

    public Company? Company { get; set; }
    public EarningsEvent? EarningsEvent { get; set; }
}
