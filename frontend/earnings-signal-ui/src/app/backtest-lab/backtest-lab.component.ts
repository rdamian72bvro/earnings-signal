import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { BacktestRunRequest } from '../models/backtest-run-request.model';
import { BacktestRunResult } from '../models/backtest-run-result.model';
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
export class BacktestLabComponent {
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
  errorMessage = '';
  result: BacktestRunResult | null = null;

  constructor(private readonly marketDataService: MarketDataService) {}

  runBacktest(): void {
    if (this.holdingDays < 1 || this.holdingDays > 20) {
      this.errorMessage = 'Holding days must be between 1 and 20.';
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
          this.result = result;
          this.isRunning = false;
        },
        error: (error: { error?: { error?: string } }) => {
          this.errorMessage = error?.error?.error ?? 'Could not run backtest.';
          this.isRunning = false;
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

  trackTrade(_index: number, trade: BacktestTrade): string {
    return trade.id;
  }
}
