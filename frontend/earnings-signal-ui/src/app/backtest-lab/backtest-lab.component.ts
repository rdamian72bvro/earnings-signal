import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BacktestRunRequest } from '../models/backtest-run-request.model';
import { BacktestRunResult } from '../models/backtest-run-result.model';
import { BacktestRun } from '../models/backtest-run.model';
import { BacktestTrade } from '../models/backtest-trade.model';
import { MarketDataService } from '../services/market-data.service';

@Component({
  selector: 'app-backtest-lab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './backtest-lab.component.html',
  styleUrl: './backtest-lab.component.scss'
})
export class BacktestLabComponent implements OnInit {
  strategyType = 'CleanMissShort';
  holdingDays = 3;
  minReactionPct = -2;
  fromDate: string | null = null;
  toDate: string | null = null;

  isRunning = false;
  isLoadingRuns = true;
  isLoadingTrades = false;
  errorMessage = '';

  recentRuns: BacktestRun[] = [];
  selectedRun: BacktestRun | null = null;
  selectedTrades: BacktestTrade[] = [];
  lastRunRequest: BacktestRunRequest | null = null;

  constructor(private readonly marketDataService: MarketDataService) {}

  ngOnInit(): void {
    this.loadRecentRuns();
  }

  runBacktest(): void {
    this.errorMessage = '';
    this.isRunning = true;

    const request: BacktestRunRequest = {
      strategyType: this.strategyType,
      holdingDays: this.holdingDays,
      fromDate: this.fromDate,
      toDate: this.toDate,
      minReactionPct: this.minReactionPct
    };

    this.lastRunRequest = { ...request };

    this.marketDataService.runBacktest(request).subscribe({
      next: (result: BacktestRunResult) => {
        this.selectedRun = result.run;
        this.selectedTrades = result.trades;
        this.isRunning = false;
        this.loadRecentRuns();
      },
      error: (error: { error?: { error?: string } }) => {
        this.errorMessage = error?.error?.error ?? 'Could not run backtest. Please try again.';
        this.isRunning = false;
      }
    });
  }

  loadTrades(run: BacktestRun): void {
    this.selectedRun = run;
    this.isLoadingTrades = true;
    this.errorMessage = '';

    this.marketDataService.getBacktestTrades(run.id).subscribe({
      next: (trades: BacktestTrade[]) => {
        this.selectedTrades = trades;
        this.isLoadingTrades = false;
      },
      error: () => {
        this.errorMessage = 'Could not load trades for this backtest run.';
        this.isLoadingTrades = false;
      }
    });
  }

  formatDate(dateValue: string | null): string {
    if (!dateValue) {
      return '-';
    }

    const [yearText, monthText, dayText] = dateValue.split('-');
    const year = Number(yearText);
    const month = Number(monthText);
    const day = Number(dayText);

    if (!year || !month || !day) {
      return dateValue;
    }

    return new Date(year, month - 1, day).toLocaleDateString(undefined, {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  }

  exportTradesCsv(): void {
    if (this.selectedTrades.length === 0 || !this.selectedRun) {
      return;
    }

    const header = [
      'ticker',
      'direction',
      'setupType',
      'entryDate',
      'entryPrice',
      'exitDate',
      'exitPrice',
      'returnPct',
      'notes'
    ];

    const rows = this.selectedTrades.map((trade) => [
      trade.ticker,
      trade.direction,
      trade.setupType,
      trade.entryDate,
      trade.entryPrice.toString(),
      trade.exitDate,
      trade.exitPrice.toString(),
      trade.returnPct.toString(),
      trade.notes.replaceAll('"', '""')
    ]);

    const csvLines = [header.join(','), ...rows.map((row) => row.map((value) => `"${value}"`).join(','))];
    const csv = csvLines.join('\n');

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const downloadUrl = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = downloadUrl;
    link.download = `backtest-${this.selectedRun.id}.csv`;
    link.click();
    URL.revokeObjectURL(downloadUrl);
  }

  exportRunHistoryCsv(): void {
    if (this.recentRuns.length === 0) {
      return;
    }

    const header = [
      'id',
      'createdAtUtc',
      'strategyType',
      'holdingDays',
      'fromDate',
      'toDate',
      'totalEventsEvaluated',
      'totalTrades',
      'winningTrades',
      'winRatePct',
      'averageReturnPct'
    ];

    const rows = this.recentRuns.map((run) => [
      run.id,
      run.createdAtUtc,
      run.strategyType,
      run.holdingDays.toString(),
      run.fromDate ?? '',
      run.toDate ?? '',
      run.totalEventsEvaluated.toString(),
      run.totalTrades.toString(),
      run.winningTrades.toString(),
      run.winRatePct.toString(),
      run.averageReturnPct.toString()
    ]);

    const csvLines = [header.join(','), ...rows.map((row) => row.map((value) => `"${value}"`).join(','))];
    const csv = csvLines.join('\n');

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const downloadUrl = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = downloadUrl;
    link.download = 'backtest-history.csv';
    link.click();
    URL.revokeObjectURL(downloadUrl);
  }

  private loadRecentRuns(): void {
    this.marketDataService.getBacktestRuns().subscribe({
      next: (runs: BacktestRun[]) => {
        this.recentRuns = runs;
        this.isLoadingRuns = false;
      },
      error: () => {
        this.errorMessage = 'Could not load backtest history.';
        this.isLoadingRuns = false;
      }
    });
  }
}
