import { Planet } from '../planets/planet.model';

export type NewMission = {
  name: string;
  date: string;
  description: string | null;
  planetId: number;
};

export type Mission = NewMission & {
  id: number;
  planet?: Planet;
}
