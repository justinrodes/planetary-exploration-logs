import {
  ChangeDetectionStrategy, Component, effect, inject, Input, OnInit, TemplateRef, ViewChild
} from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MatDialogRef
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiCall } from '../../shared/api-call.class';
import { UtilityService } from '../../shared/utility.service';
import { NewPlanet, Planet } from '../planet.model';
import { PlanetService } from '../planet.service';
import { PlanetStore } from '../planet.store';

@Component({
  selector: 'app-planet-dialog',
  imports: [
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MatFormFieldModule,
    MatInputModule, MatProgressSpinnerModule, ReactiveFormsModule
  ],
  templateUrl: './planet-dialog.component.html',
  styleUrl: './planet-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlanetDialogComponent implements OnInit {
  constructor() {
    effect(() => {
      const data = this.apiGet?.data();

      if (data) {
        this.planetForm.setValue({
          name: data.name,
          type: data.type,
          climate: data.climate,
          terrain: data.terrain,
          population: data.population
        });
      }
    });
  }

  readonly #router = inject(Router);
  readonly #route = inject(ActivatedRoute);
  readonly #planetService = inject(PlanetService);
  readonly #planetStore = inject(PlanetStore);
  readonly #dialog = inject(MatDialog);
  readonly #snackBar = inject(MatSnackBar);
  readonly #util = inject(UtilityService);

  readonly planetForm = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    type: new FormControl('', { nonNullable: true }),
    climate: new FormControl(''),
    terrain: new FormControl(''),
    population: new FormControl('')
  });

  #createMode = false;
  #dialogRef: MatDialogRef<any> | undefined;
  #templateElement: TemplateRef<any> | undefined;
  apiGet: ApiCall<Planet> | undefined;
  apiNonGet: ApiCall<number> | undefined;

  @Input() set id(id: string) {
    if (id === 'new') {
      this.#createMode = true;
    }
    else if (this.#util.isPositiveInteger(id)) {
      this.apiGet = new ApiCall<Planet>(this.#planetService.getPlanet(+id));
    }
  }

  @ViewChild('dialogTemplate') set dialogTemplate(template: TemplateRef<any>) {
    this.#templateElement = template;
    this.#dialogRef = this.#dialog.open(this.#templateElement, {
      disableClose: true,
      width: '28rem',
      height: '38rem'
    });

    this.#dialogRef.afterClosed().subscribe(_ => {
      this.#router.navigate(['../'], { relativeTo: this.#route });
    });
  }

  get busy(): boolean {
    return (this.apiGet === undefined && !this.#createMode) ||
      this.apiGet?.loading() ||
      this.apiNonGet?.loading() ||
      this.apiNonGet?.data() !== undefined;
  }

  ngOnInit(): void {
    this.apiGet?.action$.subscribe({
      complete: () => {
        if (this.apiGet!.hasError()) {
          this.#snackBar.open('Error: Unable to retrieve planet', 'Dismiss', { panelClass: 'error-notification' });
        }
      }
    });
  }

  onDelete(): void {
    const planetId = this.apiGet!.data()!.id;
    this.apiNonGet = new ApiCall<number>(this.#planetService.deletePlanet(planetId));

    this.apiNonGet.action$.subscribe({
      complete: () => {
        if (this.apiNonGet!.hasError()) {
          this.#snackBar.open('Error: Unable to remove planet', 'Dismiss', { panelClass: 'error-notification' });
        }
        else {
          this.#planetService.changePlanet({ action: 'delete', id: planetId });
          this.#planetStore.removePlanet(planetId);
          this.#dialogRef?.close();
        }
      }
    });
  }

  onSubmit(): void {
    Object.values(this.planetForm.controls).forEach(control => {
      this.#util.trimInputValue(control);
    });

    if (!this.planetForm.valid) {
      this.#util.markAllControlsAsTouched(this.planetForm);
      return;
    }

    if (this.#createMode) {
      // Add a new planet
      const newPlanet = this.planetForm.value as NewPlanet;

      this.apiNonGet = new ApiCall<number>(this.#planetService.addPlanet(newPlanet));

      this.apiNonGet.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to add planet', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            const planet: Planet = { ...newPlanet, id: this.apiNonGet?.data()! };
            this.#planetService.changePlanet({ action: 'create', id: planet.id, planet });
            this.#planetStore.addPlanet(planet);
            this.#dialogRef?.close();
          }
        }
      });
    }
    else {
      // Update the existing planet
      const planet = {
        ...this.planetForm.value,
        id: this.apiGet?.data()?.id ?? 0
      } as Planet;

      this.apiNonGet = new ApiCall<number>(this.#planetService.updatePlanet(planet));

      this.apiNonGet.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to save planet', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            this.#planetService.changePlanet({ action: 'update', id: planet.id, planet });
            this.#planetStore.updatePlanet(planet);
            this.#dialogRef?.close();
          }
        }
      });
    }
  }
}
