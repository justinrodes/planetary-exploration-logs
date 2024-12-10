import { inject, Injectable } from '@angular/core';
import { exhaustMap, pipe, tap } from 'rxjs';
import { signalState, patchState } from '@ngrx/signals';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { tapResponse } from '@ngrx/operators';
import { PlanetService } from './planet.service';
import { Planet, PlanetDropdownItem } from './planet.model';

type PlanetsState = { planets: PlanetDropdownItem[]; loading: boolean };

const initialState: PlanetsState = {
  planets: [],
  loading: false
};

@Injectable({
  providedIn: 'root'
})
export class PlanetStore {
  readonly #planetService = inject(PlanetService);
  readonly #state = signalState(initialState);

  readonly planets = this.#state.planets;
  readonly loading = this.#state.loading;

  readonly loadPlanets = rxMethod<void>(
    pipe(
      tap(() => patchState(this.#state, { loading: true })),
      exhaustMap(() => {
        return this.#planetService.getPlanetDropdown().pipe(
          tapResponse({
            next: (response) => {
              const planets = response.data;
              patchState(this.#state, { planets })
            },
            error: console.error,
            finalize: () => patchState(this.#state, { loading: false })
          })
        );
      })
    )
  );

  readonly addPlanet = (planet: Planet) => patchState(this.#state, (state) => ({
    planets: [...state.planets, { id: planet.id, name: planet.name }].sort(this.#planetNameComparator)
  }));

  readonly updatePlanet = (planet: Planet) => patchState(this.#state, (state) => ({
    planets: state.planets.map(p => {
      if (p.id === planet.id) {
        return { id: p.id, name: planet.name };
      }

      return p;
    }).sort(this.#planetNameComparator)
  }));

  readonly removePlanet = (id: number) => patchState(this.#state, (state) => ({
    planets: state.planets.filter(p => p.id !== id)
  }));

  readonly #planetNameComparator = (planetA: PlanetDropdownItem, planetB: PlanetDropdownItem) => {
    if (planetA.name < planetB.name) {
      return -1;
    }
    if (planetA.name > planetB.name) {
      return 1;
    }
    return 0;
  }
}
