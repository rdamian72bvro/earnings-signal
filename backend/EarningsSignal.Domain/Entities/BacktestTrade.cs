namespace EarningsSignal.Domain.Entities;

public class BacktestTrade
{
    public Guid Id { get; set; }
    public Guid BacktestRunId { get; set; }
    public Guid EarningsEventId { get; set; }
    public Guid CompanyId { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string SetupType { get; set; } = string.Empty;
    public DateOnly EntryDate { get; set; }
    public decimal EntryPrice { get; set; }
    public DateOnly ExitDate { get; set; }
    public decimal ExitPrice { get; set; }
    public decimal ReturnPct { get; set; }
    public string Notes { get; set; } = string.Empty;

    public BacktestRun? BacktestRun { get; set; }
}
