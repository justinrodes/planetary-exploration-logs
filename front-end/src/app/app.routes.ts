import { Routes } from '@angular/router';
import { MissionListComponent } from './missions/mission-list/mission-list.component';
import { PlanetListComponent } from './planets/planet-list/planet-list.component';
import { Path } from './shared/path.enum';

export const routes: Routes = [
  {
    path: '',
    title: 'Planetary Exploration Logs',
    redirectTo: `/${Path.Missions}`,
    pathMatch: 'full'
  },
  {
    path: Path.Missions,
    title: 'Missions - Planetary Exploration Logs',
    component: MissionListComponent,
    loadChildren: () => import('./missions/missions.routes').then(m => m.missionRoutes)
  },
  {
    path: Path.Planets,
    title: 'Planets - Planetary Exploration Logs',
    component: PlanetListComponent,
    loadChildren: () => import('./planets/planets.routes').then(m => m.planetRoutes)
  },
  { path: '**', redirectTo: `/${Path.Missions}` }
];
