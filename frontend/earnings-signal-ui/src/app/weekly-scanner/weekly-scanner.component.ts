import { CommonModule, DatePipe } from '@angular/common';
import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { LiveSignal } from '../models/live-signal.model';
import { UpcomingEarnings } from '../models/upcoming-earnings.model';
import { MarketDataService } from '../services/market-data.service';

@Component({
  selector: 'app-weekly-scanner',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './weekly-scanner.component.html',
  styleUrl: './weekly-scanner.component.scss'
})
export class WeeklyScannerComponent implements OnInit {
  private readonly destroyRef = inject(DestroyRef);

  upcomingEarnings: UpcomingEarnings[] = [];
  liveSignals: LiveSignal[] = [];
  isLoadingEarnings = true;
  isLoadingSignals = true;
  earningsError = '';
  signalsError = '';

  constructor(private readonly marketDataService: MarketDataService) {}

  ngOnInit(): void {
    this.loadUpcomingEarnings();
    this.loadLiveSignals();
  }

  private loadUpcomingEarnings(): void {
    this.marketDataService
      .getUpcomingEarnings()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (data: UpcomingEarnings[]) => {
          this.upcomingEarnings = data;
          this.isLoadingEarnings = false;
        },
        error: () => {
          this.earningsError = 'Could not load upcoming earnings from the API.';
          this.isLoadingEarnings = false;
        }
      });
  }

  private loadLiveSignals(): void {
    this.marketDataService
      .getLiveSignals()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (data: LiveSignal[]) => {
          this.liveSignals = data;
          this.isLoadingSignals = false;
        },
        error: () => {
          this.signalsError = 'Could not load live signals from the API.';
          this.isLoadingSignals = false;
        }
      });
  }

  formatReportDate(reportDate: string): string {
    const [yearText, monthText, dayText] = reportDate.split('-');
    const year = Number(yearText);
    const month = Number(monthText);
    const day = Number(dayText);

    if (!year || !month || !day) {
      return reportDate;
    }

    return new Date(year, month - 1, day).toLocaleDateString(undefined, {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  }
}
