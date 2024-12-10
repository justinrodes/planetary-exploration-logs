export type PlanetDropdownItem = {
  id: number;
  name: string;
};

export type NewPlanet = {
  name: string;
  type: string;
  climate: string | null;
  terrain: string | null;
  population: string | null;
};

export type Planet = NewPlanet & {
  id: number;
};
