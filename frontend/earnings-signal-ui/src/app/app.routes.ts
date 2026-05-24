import { Routes } from '@angular/router';
import { WeeklyScannerComponent } from './weekly-scanner/weekly-scanner.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'scanner', pathMatch: 'full' },
  { path: 'scanner', component: WeeklyScannerComponent },
  { path: '**', redirectTo: '' }
];
