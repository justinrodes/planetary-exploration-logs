import { ChangeDetectionStrategy, Component, effect, inject, Signal } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiCall } from '../../shared/api-call.class';
import { UtilityService } from '../../shared/utility.service';
import { Discovery, DiscoveryType, NewDiscovery } from '../discovery.model';
import { DiscoveryService } from '../discovery.service';
import { DiscoveryTypeStore } from '../discovery-type.store';

type DialogData = {
  missionId: number,
  discovery: Discovery
}

@Component({
  selector: 'app-discovery-dialog',
  imports: [
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MatFormFieldModule,
    MatInputModule, MatProgressSpinnerModule, MatSelectModule, ReactiveFormsModule
  ],
  templateUrl: './discovery-dialog.component.html',
  styleUrl: './discovery-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DiscoveryDialogComponent {
  constructor() {
    this.discoveryTypes = this.#discoveryTypeStore.discoveryTypes;

    effect(() => {
      if (!this.#discoveryTypeStore.loading()) {
        this.discoveryForm.controls.discoveryTypeId.enable();
      }
    });

    if (this.data.discovery) {
      this.discoveryForm.setValue({
        name: this.data.discovery.name,
        discoveryTypeId: this.data.discovery.discoveryTypeId,
        location: this.data.discovery.location,
        description: this.data.discovery.description
      });
    }
  }
  readonly data: DialogData = inject(MAT_DIALOG_DATA);
  readonly #discoveryService = inject(DiscoveryService);
  readonly #discoveryTypeStore = inject(DiscoveryTypeStore);
  readonly #dialogRef = inject(MatDialogRef<DiscoveryDialogComponent>);
  readonly #snackBar = inject(MatSnackBar);
  readonly #util = inject(UtilityService);
  readonly discoveryTypes: Signal<DiscoveryType[]>;

  readonly discoveryForm = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    discoveryTypeId: new FormControl({ value: -1, disabled: true }, {
      nonNullable: true,
      validators: [Validators.required, Validators.min(1)]
    }),
    location: new FormControl(''),
    description: new FormControl('')
  });

  apiNonGet: ApiCall<number> | undefined;

  get busy(): boolean {
    return this.apiNonGet?.loading() || this.apiNonGet?.data() !== undefined
  }

  onDelete(): void {
    const discoveryId = this.data.discovery.id;
    this.apiNonGet = new ApiCall<number>(this.#discoveryService.deleteDiscovery(this.data.missionId, discoveryId));

    this.apiNonGet.action$.subscribe({
      complete: () => {
        if (this.apiNonGet!.hasError()) {
          this.#snackBar.open('Error: Unable to remove discovery', 'Dismiss', { panelClass: 'error-notification' });
        }
        else {
          this.#dialogRef?.close({ removedDiscoveryId: discoveryId });
        }
      }
    });
  }

  onSubmit(): void {
    this.#util.trimInputValue(this.discoveryForm.controls.name);
    this.#util.trimInputValue(this.discoveryForm.controls.location);
    this.#util.trimInputValue(this.discoveryForm.controls.description);

    if (!this.discoveryForm.valid) {
      this.#util.markAllControlsAsTouched(this.discoveryForm);
      return;
    }

    if (this.data.discovery === undefined) {
      // Add a new discovery
      const newDiscovery: NewDiscovery = {
        name: this.discoveryForm.controls.name.value,
        discoveryTypeId: this.discoveryForm.controls.discoveryTypeId.value,
        location: this.discoveryForm.controls.location.value,
        description: this.discoveryForm.controls.description.value,
        missionId: this.data.missionId
      };

      this.apiNonGet = new ApiCall<number>(this.#discoveryService.addDiscovery(newDiscovery));

      this.apiNonGet?.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to add discovery', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            const discovery: Discovery = {
              ...newDiscovery,
              discoveryType: {
                id: newDiscovery.discoveryTypeId,
                name: this.#discoveryTypeName(newDiscovery.discoveryTypeId)
              },
              id: this.apiNonGet?.data()!
            }

            this.#dialogRef?.close({ newDiscovery: discovery });
          }
        }
      });
    }
    else {
      // Update the existing discovery
      const discovery: Discovery = {
        id: this.data.discovery.id,
        name: this.discoveryForm.controls.name.value,
        discoveryTypeId: this.discoveryForm.controls.discoveryTypeId.value,
        location: this.discoveryForm.controls.location.value,
        description: this.discoveryForm.controls.description.value,
        missionId: this.data.missionId
      };

      this.apiNonGet = new ApiCall<number>(this.#discoveryService.updateDiscovery(discovery));

      this.apiNonGet?.action$.subscribe({
        complete: () => {
          if (this.apiNonGet!.hasError()) {
            this.#snackBar.open('Error: Unable to save discovery', 'Dismiss', { panelClass: 'error-notification' });
          }
          else {
            this.#dialogRef?.close({
              editedDiscovery: {
                ...discovery,
                discoveryType: {
                  id: discovery.discoveryTypeId,
                  name: this.#discoveryTypeName(discovery.discoveryTypeId)
                }
              }
            });
          }
        }
      });
    }
  }

  readonly #discoveryTypeName = (id: number) => this.discoveryTypes().find(t => t.id === id)?.name ?? ''
}
