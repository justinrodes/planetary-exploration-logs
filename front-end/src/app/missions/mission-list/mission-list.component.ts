import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiCall } from '../../shared/api-call.class';
import { Path } from '../../shared/path.enum';
import { Planet } from '../../planets/planet.model';
import { PlanetStore } from '../../planets/planet.store';
import { Mission } from '../mission.model';
import { MissionService } from '../mission.service';
import { MissionPanelComponent } from "../mission-panel/mission-panel.component";

@Component({
  selector: 'app-mission-list',
  imports: [MatButtonModule, MatProgressSpinnerModule, MissionPanelComponent, RouterLink, RouterOutlet],
  templateUrl: './mission-list.component.html',
  styleUrl: './mission-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MissionListComponent implements OnInit {
  readonly #missionService = inject(MissionService);
  readonly #planetStore = inject(PlanetStore);
  readonly #snackBar = inject(MatSnackBar);
  readonly apiGet = new ApiCall<Mission[]>(this.#missionService.getMissions());
  readonly addPath = `/${Path.Missions}/new`

  ngOnInit(): void {
    this.apiGet?.action$.subscribe({
      complete: () => {
        if (this.apiGet!.hasError()) {
          this.#snackBar.open('Error: Unable to retrieve missions', 'Dismiss', { panelClass: 'error-notification' });
        }
      }
    });

    this.#missionService.missionChange$.subscribe(changed => {
      switch (changed.action) {
        case 'create':
          changed.mission!.planet = this.#getPlanet(changed.id);
          this.apiGet.data.update(missions => [changed.mission!, ...(missions ?? [])]);
          break;
        case 'update':
          changed.mission!.planet = this.#getPlanet(changed.id);
          this.apiGet.data.update(missions => missions!.map(m => (m.id === changed.id) ? changed.mission! : m));
          break;
        case 'delete':
          this.apiGet.data.update(missions => missions!.filter(m => m.id !== changed.id));
          break;
      }
    });
  }

  readonly #getPlanet = (planetId: number): Planet => ({
    id: planetId,
    name: this.#planetStore.planets().find(p => p.id === planetId)?.name ?? '',
    type: '',
    climate: null,
    terrain: null,
    population: null
  });
}
