export interface BacktestTrade {
  id: string;
  backtestRunId: string;
  ticker: string;
  direction: string;
  setupType: string;
  entryDate: string;
  entryPrice: number;
  exitDate: string;
  exitPrice: number;
  returnPct: number;
  notes: string;
}
