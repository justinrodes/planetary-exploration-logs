import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { DiscoveryService } from './discovery.service';
import { Discovery, DiscoveryType, NewDiscovery } from './discovery.model';
import { TestUtility } from '../shared/test-utility.class';
import { ConfigService } from '../shared/config.service';
import { ApiResponse } from '../shared/types';

const mockDiscoveries: Discovery[] = [{
  id: 1,
  name: 'Test discovery 1',
  location: null,
  description: null,
  discoveryTypeId: 1,
  missionId: 1
}, {
  id: 2,
  name: 'Test discovery 2',
  location: null,
  description: null,
  discoveryTypeId: 2,
  missionId: 1
}];

const mockDiscoveryTypes: DiscoveryType[] = [{
  id: 1,
  name: 'Test discovery type 1'
}, {
  id: 2,
  name: 'Test discovery type 2'
}]

const store: Record<string, string> = {};
const mockLocalStorage = {
  getItem: (key: string): string | null => {
    return key in store ? store[key] : null;
  },
  setItem: (key: string, value: string) => {
    store[key] = `${value}`;
  },
  removeItem: (key: string) => {
    delete store[key];
  }
};

describe('DiscoveryService', () => {
  let service: DiscoveryService;
  let config: ConfigService;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(DiscoveryService);
    config = TestBed.inject(ConfigService);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get discovery types', () => {
    spyOn(localStorage, 'getItem')
      .and.callFake(mockLocalStorage.getItem);
    spyOn(localStorage, 'setItem')
      .and.callFake(mockLocalStorage.setItem);
    spyOn(localStorage, 'removeItem')
      .and.callFake(mockLocalStorage.removeItem);
    localStorage.removeItem(config.DISCOVERY_TYPES_KEY);
    const mockApiResponse = TestUtility.successResponse(mockDiscoveryTypes);

    service.getDiscoveryTypeDropdown().subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/discovery-type/dropdown`);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);
  });

  it('should get discovery types locally if possible', () => {
    spyOn(localStorage, 'getItem')
      .and.callFake(mockLocalStorage.getItem);
    spyOn(localStorage, 'setItem')
      .and.callFake(mockLocalStorage.setItem);
    localStorage.setItem(config.DISCOVERY_TYPES_KEY, JSON.stringify(mockDiscoveryTypes));

    const mockApiResponse: ApiResponse<DiscoveryType[]> = {
      success: true,
      statusCode: 200,
      message: 'Retrieved from localStorage',
      data: mockDiscoveryTypes
    };

    service.getDiscoveryTypeDropdown().subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    httpTesting.expectNone(`${config.API_BASE_URL}/discovery-type/dropdown`);
  });

  it('should get discoveries', () => {
    const mockApiResponse = TestUtility.successResponse(mockDiscoveries);
    const missionId = 1;

    service.getDiscoveries(missionId).subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/mission/${missionId}/discovery`);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);
  });

  it('should get a discovery', () => {
    const mockApiResponse = TestUtility.successResponse(mockDiscoveries[0]);
    const missionId = 1;
    const discoveryId = 1;

    service.getDiscovery(missionId, discoveryId).subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/mission/${missionId}/discovery/${discoveryId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);
  });

  it('should add a discovery', () => {
    const mockApiResponse = TestUtility.successResponse(1);
    const missionId = 1;
    const newDiscovery: NewDiscovery = {
      name: 'New test discovery',
      discoveryTypeId: 1,
      location: null,
      description: null,
      missionId
    }

    service.addDiscovery(newDiscovery).subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/mission/${missionId}/discovery`);
    expect(req.request.method).toBe('POST');
    req.flush(mockApiResponse);
  });

  it('should update a discovery', () => {
    const mockApiResponse = TestUtility.successResponse(1);
    const missionId = 1;
    const updatedDiscovery: Discovery = {
      ...mockDiscoveries[0],
      name: 'Updated test discovery'
    }

    service.updateDiscovery(updatedDiscovery).subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/mission/${missionId}/discovery/${updatedDiscovery.id}`);
    expect(req.request.method).toBe('PUT');
    req.flush(mockApiResponse);
  });


  it('should delete a discovery', () => {
    const mockApiResponse = TestUtility.successResponse(1);
    const missionId = 1;
    const discoveryId = 1;

    service.deleteDiscovery(missionId, discoveryId).subscribe(resp => {
      expect(resp).toEqual(mockApiResponse);
    });

    const req = httpTesting.expectOne(`${config.API_BASE_URL}/mission/${missionId}/discovery/${discoveryId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockApiResponse);
  });
});
