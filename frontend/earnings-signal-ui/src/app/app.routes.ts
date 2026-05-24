import { Routes } from '@angular/router';
import { WeeklyScannerComponent } from './weekly-scanner/weekly-scanner.component';

export const appRoutes: Routes = [
  { path: '', component: WeeklyScannerComponent },
  { path: '**', redirectTo: '' }
];
