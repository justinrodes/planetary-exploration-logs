import { DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, model, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { Path } from '../../shared/path.enum';
import { DiscoveryListComponent } from "../../discoveries/discovery-list/discovery-list.component";
import { Mission } from '../mission.model';

@Component({
  selector: 'app-mission-panel',
  imports: [DiscoveryListComponent, MatButtonModule, MatExpansionModule, RouterLink],
  providers: [DatePipe],
  templateUrl: './mission-panel.component.html',
  styleUrl: './mission-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MissionPanelComponent {
  readonly #datePipe = inject(DatePipe);
  readonly panelOpenState = signal(false);
  readonly editPath = `/${Path.Missions}/`
  mission = model.required<Mission>();

  readonly formatDate = (isoDateString: string) => {
    return this.#datePipe.transform(isoDateString, 'MM/dd/yyyy');
  }
}
