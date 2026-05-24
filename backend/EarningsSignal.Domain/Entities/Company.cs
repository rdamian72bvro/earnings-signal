namespace EarningsSignal.Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;

    public ICollection<EarningsEvent> EarningsEvents { get; set; } = new List<EarningsEvent>();
    public ICollection<DailyPrice> DailyPrices { get; set; } = new List<DailyPrice>();
    public ICollection<Signal> Signals { get; set; } = new List<Signal>();
}
