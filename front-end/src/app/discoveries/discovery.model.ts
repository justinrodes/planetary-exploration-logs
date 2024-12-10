export type DiscoveryType = {
  id: number;
  name: string;
  description?: string;
};

export type NewDiscovery = {
  name: string;
  location: string | null;
  description: string | null;
  discoveryTypeId: number;
  missionId: number;
};

export type Discovery = NewDiscovery & {
  id: number;
  discoveryType?: DiscoveryType;
};
