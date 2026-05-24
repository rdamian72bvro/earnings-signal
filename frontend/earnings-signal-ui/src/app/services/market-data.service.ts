import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BacktestRunRequest } from '../models/backtest-run-request.model';
import { BacktestRunResult } from '../models/backtest-run-result.model';
import { LiveSignal } from '../models/live-signal.model';
import { UpcomingEarnings } from '../models/upcoming-earnings.model';

@Injectable({
  providedIn: 'root'
})
export class MarketDataService {
  private readonly apiBaseUrl = '/api';

  constructor(private readonly httpClient: HttpClient) {}

  getUpcomingEarnings(): Observable<UpcomingEarnings[]> {
    return this.httpClient.get<UpcomingEarnings[]>(`${this.apiBaseUrl}/earnings/upcoming`);
  }

  getLiveSignals(): Observable<LiveSignal[]> {
    return this.httpClient.get<LiveSignal[]>(`${this.apiBaseUrl}/signals/live`);
  }

  runBacktest(request: BacktestRunRequest): Observable<BacktestRunResult> {
    return this.httpClient.post<BacktestRunResult>(`${this.apiBaseUrl}/backtests/run`, request);
  }
}
