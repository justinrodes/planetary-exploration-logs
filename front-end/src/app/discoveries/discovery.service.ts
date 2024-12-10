import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { ConfigService } from '../shared/config.service';
import { ApiResponse } from '../shared/types';
import { Discovery, DiscoveryType, NewDiscovery } from './discovery.model';

@Injectable({
  providedIn: 'root'
})
export class DiscoveryService {
  constructor() {
    this.#baseUrl = `${this.#config.API_BASE_URL}/mission`;
  }

  readonly #config = inject(ConfigService);
  readonly #http = inject(HttpClient);
  readonly #baseUrl;

  readonly getDiscoveryTypeDropdown = () => {
    const discoveryTypesJson = localStorage.getItem(this.#config.DISCOVERY_TYPES_KEY);

    if (discoveryTypesJson !== null) {
      try {
        const discoveryTypes: DiscoveryType[] = JSON.parse(discoveryTypesJson);

        if (discoveryTypes.length) {
          return of({
            success: true,
            message: 'Retrieved from localStorage',
            statusCode: 200,
            data: discoveryTypes
          } as ApiResponse<DiscoveryType[]>);
        }
      }
      catch {
        // If an error occurred while getting local data, continue
      }
    }

    return this.#http.get<ApiResponse<DiscoveryType[]>>(`${this.#config.API_BASE_URL}/discovery-type/dropdown`);
  }

  readonly getDiscoveries = (missionId: number) =>
    this.#http.get<ApiResponse<Discovery[]>>(`${this.#baseUrl}/${missionId}/discovery`);

  readonly getDiscovery = (missionId: number, id: number) =>
    this.#http.get<ApiResponse<Discovery>>(`${this.#baseUrl}/${missionId}/discovery/${id}`);

  readonly addDiscovery = (discovery: NewDiscovery) =>
    this.#http.post<ApiResponse<number>>(`${this.#baseUrl}/${discovery.missionId}/discovery`, discovery);

  readonly updateDiscovery = (discovery: Discovery) =>
    this.#http.put<ApiResponse<number>>(`${this.#baseUrl}/${discovery.missionId}/discovery/${discovery.id}`, discovery);

  readonly deleteDiscovery = (missionId: number, id: number) =>
    this.#http.delete<ApiResponse<number>>(`${this.#baseUrl}/${missionId}/discovery/${id}`);
}
