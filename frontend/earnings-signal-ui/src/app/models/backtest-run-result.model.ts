import { BacktestRun } from './backtest-run.model';
import { BacktestTrade } from './backtest-trade.model';

export interface BacktestRunResult {
  run: BacktestRun;
  trades: BacktestTrade[];
}
