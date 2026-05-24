namespace EarningsSignal.Domain.Entities;

public class EarningsEvent
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public DateOnly ReportDate { get; set; }
    public string ReportTime { get; set; } = string.Empty;
    public decimal ExpectationPressureScore { get; set; }
    public string PreSignal { get; set; } = string.Empty;

    public Company? Company { get; set; }
    public EarningsEstimate? Estimate { get; set; }
    public EarningsActual? Actual { get; set; }
    public ICollection<Signal> Signals { get; set; } = new List<Signal>();
}
