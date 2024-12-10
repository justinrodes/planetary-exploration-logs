import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Path } from './shared/path.enum';
import { DiscoveryTypeStore } from './discoveries/discovery-type.store';
import { PlanetStore } from './planets/planet.store';

type NavTab = {
  route: string,
  label: string,
  icon: string,
  exact: boolean
};

@Component({
  selector: 'app-root',
  imports: [MatIconModule, MatTabsModule, MatToolbarModule, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  readonly #planetStore = inject(PlanetStore);
  readonly #discoveryTypeStore = inject(DiscoveryTypeStore);
  readonly tabs: NavTab[] = [
    { route: `/${Path.Missions}`, label: 'Missions', icon: 'explore', exact: false },
    { route: `/${Path.Planets}`, label: 'Planets', icon: 'planet', exact: false }
  ];

  ngOnInit(): void {
    this.#discoveryTypeStore.loadDiscoveryTypes();
    this.#planetStore.loadPlanets();
  }
}
