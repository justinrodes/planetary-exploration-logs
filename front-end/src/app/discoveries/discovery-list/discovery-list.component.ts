import { ChangeDetectionStrategy, Component, inject, model, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ApiCall } from '../../shared/api-call.class';
import { DiscoveryDialogComponent } from '../discovery-dialog/discovery-dialog.component';
import { Discovery } from '../discovery.model';
import { DiscoveryService } from '../discovery.service';

type DialogResult = {
  newDiscovery?: Discovery,
  editedDiscovery?: Discovery,
  removedDiscoveryId?: number
}

@Component({
  selector: 'app-discovery-list',
  imports: [MatButtonModule, MatCardModule, MatIconModule, MatProgressSpinnerModule, MatTableModule, MatTooltipModule],
  templateUrl: './discovery-list.component.html',
  styleUrl: './discovery-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DiscoveryListComponent implements OnInit {
  missionId = model.required<number>();
  readonly #discoveryService = inject(DiscoveryService);
  readonly #dialog = inject(MatDialog);
  readonly #snackBar = inject(MatSnackBar);
  readonly columnsToDisplay = ['name', 'type', 'location', 'description'];
  #dialogRef: MatDialogRef<DiscoveryDialogComponent, DialogResult> | undefined;
  apiGet: ApiCall<Discovery[]> | undefined;

  ngOnInit(): void {
    this.apiGet = new ApiCall<Discovery[]>(this.#discoveryService.getDiscoveries(this.missionId()));

    this.apiGet?.action$.subscribe({
      complete: () => {
        if (this.apiGet!.hasError()) {
          this.#snackBar.open('Error: Unable to retrieve discoveries', 'Dismiss', { panelClass: 'error-notification' });
        }
      }
    });
  }

  onRowClick(row: any) {
    console.dir(row);
  }

  openDialog(discovery?: Discovery): void {
    this.#dialogRef = this.#dialog.open(DiscoveryDialogComponent, {
      data: { missionId: this.missionId(), discovery },
      disableClose: true,
      width: '28rem',
      height: '37rem'
    });

    this.#dialogRef.afterClosed().subscribe(result => {
      if (result === undefined) {
        return;
      }

      if (result.newDiscovery) {
        this.apiGet?.data.update(discoveries => {
          return [result.newDiscovery!, ...(discoveries ?? [])].sort(this.#discoveryIdComparator)
        });
      }

      if (result.editedDiscovery) {
        this.apiGet?.data.update(discoveries => {
          return discoveries?.map(d => {
            return (d.id === result.editedDiscovery!.id) ? result.editedDiscovery! : d
          }).sort(this.#discoveryIdComparator);
        });
      }

      if (result.removedDiscoveryId) {
        this.apiGet?.data.update(discoveries => discoveries!.filter(d => d.id !== result.removedDiscoveryId));
      }
    });
  }

  readonly #discoveryIdComparator = (discoveryA: Discovery, discoveryB: Discovery) => {
    if (discoveryA.id < discoveryB.id) {
      return -1;
    }
    if (discoveryA.id > discoveryB.id) {
      return 1;
    }
    return 0;
  }
}
