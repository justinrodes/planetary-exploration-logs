import {
  ChangeDetectionStrategy, Component, effect, inject, Input, OnInit, Signal, TemplateRef, ViewChild
} from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MatDialogRef
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { provideDateFnsAdapter, MAT_DATE_FNS_FORMATS } from '@angular/material-date-fns-adapter';
import { ActivatedRoute, Router } from '@angular/router';
import { formatISO, parseISO } from 'date-fns';
import { enUS } from 'date-fns/locale';
import { ApiCall } from '../../shared/api-call.class';
import { UtilityService } from '../../shared/utility.service';
import { PlanetDropdownItem } from '../../planets/planet.model';
import { PlanetStore } from '../../planets/planet.store';
import { NewMission, Mission } from '../mission.model';
import { MissionService } from '../mission.service';

@Component({
  selector: 'app-mission-dialog',
  imports: [
    MatButtonModule, MatDatepickerModule, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle,
    MatFormFieldModule, MatInputModule, MatProgressSpinnerModule, MatSelectModule, ReactiveFormsModule
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: enUS },
    { provide: MAT_DATE_FORMATS, useValue: MAT_DATE_FNS_FORMATS },
    provideDateFnsAdapter()
  ],
  templateUrl: './mission-dialog.component.html',
  styleUrl: './mission-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MissionDialogComponent implements OnInit {
  constructor() {
    this.planets = this.#planetStore.planets;

    effect(() => {
      if (!this.#planetStore.loading()) {
        this.missionForm.controls.planetId.enable();
      }
    });

    effect(() => {
      const data = this.apiGet?.data();

      if (data) {
        this.missionForm.setValue({
          name: data.name,
          date: parseISO(data.date),
          planetId: data.planetId,
          description: data.description
        });
      }
    });
  }

  readonly #router = inject(Router);
  readonly #route = inject(ActivatedRoute);
  readonly #missionService = inject(MissionService);
  readonly #dialog = inject(MatDialog);
  readonly #snackBar = inject(MatSnackBar);
  readonly #planetStore = inject(PlanetStore);
  readonly #util = inject(UtilityService);
  readonly planets: Signal<PlanetDropdownItem[]>;

  readonly missionForm = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    date: new FormControl(new Date(), { nonNullable: true }),
    planetId: new FormControl({ value: -1, disabled: true }, {
      nonNullable: true,
      validators: [Validators.required, Validators.min(1)]
    }),
    description: new FormControl('')
  });

  #createMode = false;
  #dialogRef: MatDialogRef<any> | undefined;
  #templateElement: TemplateRef<any> | undefined;
  apiGet: ApiCall<Mission> | undefined;
  apiNonGet: ApiCall<number> | undefined;

  @Input() set id(id: string) {
    if (id === 'new') {
      this.#createMode = true;
    }
    else if (this.#util.isPositiveInteger(id)) {
      this.apiGet = new ApiCall<Mission>(this.#missionService.getMission(+id));
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
          this.#snackBar.open('Error: Unable to retrieve mission', 'Dismiss', { panelClass: 'error-notification' });
        }
      }
    });
  }

  onDelete(): void {
    const missionId = this.apiGet!.data()!.id;
    this.apiNonGet = new ApiCall<number>(this.#missionService.deleteMission(missionId));

    this.apiNonGet.action$.subscribe({
      complete: () => {
        if (this.apiNonGet!.hasError()) {
          this.#snackBar.open('Error: Unable to remove mission', 'Dismiss', { panelClass: 'error-notification' });
        }
        else {
          this.#missionService.changeMission({ action: 'delete', id: missionId });
          this.#dialogRef?.close();
        }
      }
    });
  }

  onSubmit(): void {
    this.#util.trimInputValue(this.missionForm.controls.name);
    this.#util.trimInputValue(this.missionForm.controls.description);

    if (!this.missionForm.valid) {
      this.#util.markAllControlsAsTouched(this.missionForm);
      return;
    }

    if (this.#createMode) {
      // Add a new mission
      const newMission: NewMission = {
        name: this.missionForm.controls.name.value,
        date: formatISO(this.missionForm.controls.date.value, { representation: 'date' }),
        planetId: this.missionForm.controls.planetId.value,
        description: this.missionForm.controls.description.value
      };

      this.apiNonGet = new ApiCall<number>(this.#missionService.addMission(newMission));

      this.apiNonGet?.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to add mission', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            const mission = { ...newMission, id: this.apiNonGet?.data()! };
            this.#missionService.changeMission({ action: 'create', id: mission.id, mission });
            this.#dialogRef?.close();
          }
        }
      });
    }
    else {
      // Update the existing mission
      const mission: Mission = {
        name: this.missionForm.controls.name.value,
        date: formatISO(this.missionForm.controls.date.value, { representation: 'date' }),
        planetId: this.missionForm.controls.planetId.value,
        description: this.missionForm.controls.description.value,
        id: this.apiGet?.data()?.id ?? 0
      };

      this.apiNonGet = new ApiCall<number>(this.#missionService.updateMission(mission));

      this.apiNonGet?.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to save mission', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            this.#missionService.changeMission({ action: 'update', id: mission.id, mission });
            this.#dialogRef?.close();
          }
        }
      });
    }
  }
}
