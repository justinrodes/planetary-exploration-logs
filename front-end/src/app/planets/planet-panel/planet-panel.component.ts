import { ChangeDetectionStrategy, Component, model, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { Path } from '../../shared/path.enum';
import { Planet } from '../planet.model';

@Component({
  selector: 'app-planet-panel',
  imports: [MatButtonModule, MatExpansionModule, RouterLink],
  templateUrl: './planet-panel.component.html',
  styleUrl: './planet-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlanetPanelComponent {
  readonly panelOpenState = signal(false);
  readonly editPath = `/${Path.Planets}/`
  planet = model.required<Planet>();
}
