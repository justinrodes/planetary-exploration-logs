import { Mission } from '../missions/mission.model';
import { Planet } from '../planets/planet.model';

export type ApiResponse<T> = {
  success: boolean;
  message: string;
  statusCode: number;
  data: T
};

type Action = 'create' | 'update' | 'delete';

export type PlanetChange = {
  action: Action,
  id: number,
  planet?: Planet
}

export type MissionChange = {
  action: Action,
  id: number,
  mission?: Mission
}
