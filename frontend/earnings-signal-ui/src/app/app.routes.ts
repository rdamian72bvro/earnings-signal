import { Routes } from '@angular/router';
import { BacktestLabComponent } from './backtest-lab/backtest-lab.component';
import { WeeklyScannerComponent } from './weekly-scanner/weekly-scanner.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'scanner', pathMatch: 'full' },
  { path: 'scanner', component: WeeklyScannerComponent },
  { path: 'backtests', component: BacktestLabComponent },
  { path: '**', redirectTo: '' }
];
