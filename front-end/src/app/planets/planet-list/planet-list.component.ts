import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiCall } from '../../shared/api-call.class';
import { Path } from '../../shared/path.enum';
import { Planet } from '../planet.model';
import { PlanetService } from '../planet.service';
import { PlanetPanelComponent } from "../planet-panel/planet-panel.component";

@Component({
  selector: 'app-planet-list',
  imports: [MatButtonModule, MatProgressSpinnerModule, PlanetPanelComponent, RouterLink, RouterOutlet],
  templateUrl: './planet-list.component.html',
  styleUrl: './planet-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlanetListComponent implements OnInit {
  readonly #planetService = inject(PlanetService);
  readonly #snackBar = inject(MatSnackBar);
  readonly apiGet = new ApiCall<Planet[]>(this.#planetService.getPlanets());
  readonly addPath = `/${Path.Planets}/new`

  ngOnInit(): void {
    this.apiGet.action$.subscribe({
      complete: () => {
        if (this.apiGet!.hasError()) {
          this.#snackBar.open('Error: Unable to retrieve planets', 'Dismiss', { panelClass: 'error-notification' });
        }
      }
    });
    this.#planetService.planetChange$.subscribe(changed => {
      switch (changed.action) {
        case 'create':
          this.apiGet.data.update(planets => [changed.planet!, ...(planets ?? [])]);
          break;
        case 'update':
          this.apiGet.data.update(planets => planets!.map(p => (p.id === changed.id) ? changed.planet! : p));
          break;
        case 'delete':
          this.apiGet.data.update(planets => planets!.filter(p => p.id !== changed.id));
          break;
      }
    });
  }
}
