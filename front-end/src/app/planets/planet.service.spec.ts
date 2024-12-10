import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { PlanetService } from './planet.service';

describe('PlanetService', () => {
  let service: PlanetService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(PlanetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
