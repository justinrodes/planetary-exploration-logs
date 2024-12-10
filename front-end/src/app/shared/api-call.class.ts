import { WritableSignal, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { tapResponse } from '@ngrx/operators';
import { ApiResponse } from './types';

// Based on https://github.com/DevBySeb/DevBySeb/blob/feature/perform-class-2.0/src/app/perform.class.ts
export class ApiCall<T> {
  #loading = signal(false);
  #hasError = signal(false);
  #data = signal<T | undefined>(undefined);
  #action$: Observable<ApiResponse<T>>;

  get action$(): Observable<ApiResponse<T>> {
    return this.#action$;
  }

  get data(): WritableSignal<T | undefined> {
    return this.#data;
  }

  get loading(): WritableSignal<boolean> {
    return this.#loading;
  }

  get hasError(): WritableSignal<boolean> {
    return this.#hasError;
  }

  constructor(action$: Observable<ApiResponse<T>>) {
    this.#loading.set(true);
    this.#action$ = action$.pipe(
      tapResponse({
        next: (response) => {
          if (response.success) {
            this.#data.set(response.data);
          }
          else {
            this.#hasError.set(true);
            console.error(response.message);
          }
        },
        error: (err) => {
          this.#hasError.set(true);
          console.error(err);
        },
        finalize: () => this.#loading.set(false)
      })
    );
  }
}
