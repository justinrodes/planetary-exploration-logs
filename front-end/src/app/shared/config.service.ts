import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  readonly API_BASE_URL = 'http://localhost:5125/api'
  readonly DISCOVERY_TYPES_KEY = 'discovery-types';
}
