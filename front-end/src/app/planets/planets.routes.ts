import { Routes } from '@angular/router';
import { PlanetDialogComponent } from './planet-dialog/planet-dialog.component';

export const planetRoutes: Routes = [
  { path: ':id', title: 'Planet - Planetary Exploration Logs', component: PlanetDialogComponent }
];
