export interface BacktestRunRequest {
  strategyType: string;
  holdingDays: number;
  fromDate: string | null;
  toDate: string | null;
  minReactionPct: number;
}
