import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { ConfigService } from '../shared/config.service';
import { ApiResponse, MissionChange } from '../shared/types';
import { Mission, NewMission } from './mission.model';

@Injectable({
  providedIn: 'root'
})
export class MissionService {
  constructor() {
    this.#baseUrl = `${this.#config.API_BASE_URL}/mission`;
  }

  readonly #config = inject(ConfigService);
  readonly #http = inject(HttpClient);
  readonly #baseUrl;

  readonly #missionChangeSubject = new Subject<MissionChange>();
  readonly missionChange$ = this.#missionChangeSubject.asObservable();
  readonly changeMission = (change: MissionChange) => this.#missionChangeSubject.next(change);

  readonly getMissions = () => this.#http.get<ApiResponse<Mission[]>>(this.#baseUrl);

  readonly getMission = (id: number) => this.#http.get<ApiResponse<Mission>>(`${this.#baseUrl}/${id}`);

  readonly addMission = (mission: NewMission) => this.#http.post<ApiResponse<number>>(this.#baseUrl, mission);

  readonly updateMission = (mission: Mission) =>
    this.#http.put<ApiResponse<number>>(`${this.#baseUrl}/${mission.id}`, mission);

  readonly deleteMission = (id: number) => this.#http.delete<ApiResponse<number>>(`${this.#baseUrl}/${id}`);
}
