import { inject, Injectable } from '@angular/core';
import { exhaustMap, pipe, tap } from 'rxjs';
import { signalState, patchState } from '@ngrx/signals';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { tapResponse } from '@ngrx/operators';
import { ConfigService } from '../shared/config.service';
import { DiscoveryService } from './discovery.service';
import { DiscoveryType } from './discovery.model';

type DiscoveryTypesState = { discoveryTypes: DiscoveryType[]; loading: boolean };

const initialState: DiscoveryTypesState = {
  discoveryTypes: [],
  loading: false
};

@Injectable({
  providedIn: 'root'
})
export class DiscoveryTypeStore {
  readonly #config = inject(ConfigService);
  readonly #discoveryService = inject(DiscoveryService);
  readonly #state = signalState(initialState);

  readonly discoveryTypes = this.#state.discoveryTypes;
  readonly loading = this.#state.loading;

  loadDiscoveryTypes = rxMethod<void>(
    pipe(
      tap(() => patchState(this.#state, { loading: true })),
      exhaustMap(() => {
        return this.#discoveryService.getDiscoveryTypeDropdown().pipe(
          tapResponse({
            next: (response) => {
              const discoveryTypes = response.data;
              const discoveryTypesJson = localStorage.getItem(this.#config.DISCOVERY_TYPES_KEY);

              if (discoveryTypesJson === null) {
                localStorage.setItem(this.#config.DISCOVERY_TYPES_KEY, JSON.stringify(discoveryTypes));
              }

              patchState(this.#state, { discoveryTypes })
            },
            error: console.error,
            finalize: () => patchState(this.#state, { loading: false }),
          })
        );
      })
    )
  );
}
