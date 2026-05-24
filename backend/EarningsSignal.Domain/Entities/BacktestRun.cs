namespace EarningsSignal.Domain.Entities;

public class BacktestRun
{
    public Guid Id { get; set; }
    public string StrategyType { get; set; } = string.Empty;
    public int HoldingDays { get; set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public int TotalEventsEvaluated { get; set; }
    public int TotalTrades { get; set; }
    public int WinningTrades { get; set; }
    public decimal WinRatePct { get; set; }
    public decimal AverageReturnPct { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public ICollection<BacktestTrade> Trades { get; set; } = new List<BacktestTrade>();
}
