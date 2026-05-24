import { CommonModule } from '@angular/common';
import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { BacktestRunRequest } from '../models/backtest-run-request.model';
import { BacktestRunResult } from '../models/backtest-run-result.model';
import { BacktestRun } from '../models/backtest-run.model';
import { BacktestTrade } from '../models/backtest-trade.model';
import { MarketDataService } from '../services/market-data.service';

type StrategyOption = { value: string; label: string };

@Component({
  selector: 'app-backtest-lab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './backtest-lab.component.html',
  styleUrl: './backtest-lab.component.scss'
})
export class BacktestLabComponent implements OnInit {
  private readonly destroyRef = inject(DestroyRef);

  readonly strategyOptions: StrategyOption[] = [
    { value: 'CleanMissShort', label: 'Clean Miss Short' },
    { value: 'LowQualityBeatShort', label: 'Low-Quality Beat Short' },
    { value: 'BeatRejectedShort', label: 'Beat Rejected Short' },
    { value: 'BeatAndRaiseLong', label: 'Beat and Raise Long' }
  ];

  strategyType = 'CleanMissShort';
  holdingDays = 3;
  minReactionPct = -2;
  fromDate = '';
  toDate = '';

  isRunning = false;
  isLoadingRuns = true;
  isLoadingTrades = false;
  errorMessage = '';
  recentRuns: BacktestRun[] = [];
  selectedRun: BacktestRun | null = null;
  selectedTrades: BacktestTrade[] = [];

  constructor(private readonly marketDataService: MarketDataService) {}

  ngOnInit(): void {
    this.loadRecentRuns();
  }

  runBacktest(): void {
    if (this.holdingDays < 1 || this.holdingDays > 20) {
      this.errorMessage = 'Holding days must be between 1 and 20.';
      return;
    }

    if (this.fromDate && this.toDate && this.fromDate > this.toDate) {
      this.errorMessage = 'From date cannot be later than To date.';
      return;
    }

    this.errorMessage = '';
    this.isRunning = true;

    const request: BacktestRunRequest = {
      strategyType: this.strategyType,
      holdingDays: this.holdingDays,
      fromDate: this.fromDate || null,
      toDate: this.toDate || null,
      minReactionPct: this.minReactionPct
    };

    this.marketDataService
      .runBacktest(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result: BacktestRunResult) => {
          this.selectedRun = result.run;
          this.selectedTrades = result.trades;
          this.isRunning = false;
          this.loadRecentRuns(result.run.id);
        },
        error: (error: { error?: { error?: string } }) => {
          this.errorMessage = error?.error?.error ?? 'Could not run backtest.';
          this.isRunning = false;
        }
      });
  }

  selectRun(run: BacktestRun): void {
    this.selectedRun = run;
    this.loadTrades(run.id);
  }

  exportTradesCsv(): void {
    if (!this.selectedRun || this.selectedTrades.length === 0) {
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

  trackTrade(_index: number, trade: BacktestTrade): string {
    return trade.id;
  }

  trackRun(_index: number, run: BacktestRun): string {
    return run.id;
  }

  private loadRecentRuns(preferredRunId?: string): void {
    this.isLoadingRuns = true;

    this.marketDataService
      .getBacktestRuns()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (runs: BacktestRun[]) => {
          this.recentRuns = runs;
          this.isLoadingRuns = false;
          const previousSelectedId = this.selectedRun?.id;

          const runToSelect =
            (preferredRunId ? runs.find((run) => run.id === preferredRunId) : undefined)
            ?? (previousSelectedId ? runs.find((run) => run.id === previousSelectedId) : undefined)
            ?? runs[0];

          if (runToSelect) {
            const shouldLoadTrades =
              preferredRunId !== undefined
              || runToSelect.id !== previousSelectedId
              || this.selectedTrades.length === 0;

            this.selectedRun = runToSelect;

            if (shouldLoadTrades) {
              this.loadTrades(runToSelect.id);
            }
          } else {
            this.selectedRun = null;
            this.selectedTrades = [];
            this.isLoadingTrades = false;
          }
        },
        error: () => {
          this.errorMessage = 'Could not load backtest runs.';
          this.isLoadingRuns = false;
        }
      });
  }

  private loadTrades(backtestRunId: string): void {
    this.isLoadingTrades = true;

    this.marketDataService
      .getBacktestTrades(backtestRunId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (trades: BacktestTrade[]) => {
          this.selectedTrades = trades;
          this.isLoadingTrades = false;
        },
        error: () => {
          this.errorMessage = 'Could not load trades for the selected run.';
          this.isLoadingTrades = false;
        }
      });
  }
}
