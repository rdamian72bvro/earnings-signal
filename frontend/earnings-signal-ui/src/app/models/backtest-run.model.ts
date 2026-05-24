export interface BacktestRun {
  id: string;
  strategyType: string;
  holdingDays: number;
  fromDate: string | null;
  toDate: string | null;
  totalEventsEvaluated: number;
  totalTrades: number;
  winningTrades: number;
  winRatePct: number;
  averageReturnPct: number;
  createdAtUtc: string;
}
