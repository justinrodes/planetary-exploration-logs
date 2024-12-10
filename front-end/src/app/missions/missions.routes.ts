import { Routes } from '@angular/router';
import { MissionDialogComponent } from './mission-dialog/mission-dialog.component';

export const missionRoutes: Routes = [
  { path: ':id', title: 'Mission - Planetary Exploration Logs', component: MissionDialogComponent }
];
