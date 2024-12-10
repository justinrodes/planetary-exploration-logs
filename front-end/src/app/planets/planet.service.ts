import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { ConfigService } from '../shared/config.service';
import { ApiResponse, PlanetChange } from '../shared/types';
import { NewPlanet, Planet, PlanetDropdownItem } from './planet.model';

@Injectable({
  providedIn: 'root'
})
export class PlanetService {
  constructor() {
    this.#baseUrl = `${this.#config.API_BASE_URL}/planet`;
  }

  readonly #config = inject(ConfigService);
  readonly #http = inject(HttpClient);
  readonly #baseUrl;

  readonly #planetChangeSubject = new Subject<PlanetChange>();
  readonly planetChange$ = this.#planetChangeSubject.asObservable();
  readonly changePlanet = (change: PlanetChange) => this.#planetChangeSubject.next(change);

  readonly getPlanetDropdown = () => this.#http.get<ApiResponse<PlanetDropdownItem[]>>(`${this.#baseUrl}/dropdown`);

  readonly getPlanets = () => this.#http.get<ApiResponse<Planet[]>>(this.#baseUrl);

  readonly getPlanet = (id: number) => this.#http.get<ApiResponse<Planet>>(`${this.#baseUrl}/${id}`);

  readonly addPlanet = (planet: NewPlanet) => this.#http.post<ApiResponse<number>>(this.#baseUrl, planet);

  readonly updatePlanet = (planet: Planet) =>
    this.#http.put<ApiResponse<number>>(`${this.#baseUrl}/${planet.id}`, planet);

  readonly deletePlanet = (id: number) => this.#http.delete<ApiResponse<number>>(`${this.#baseUrl}/${id}`);
}
