import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { PlanetListComponent } from './planet-list.component';
import { PlanetService } from '../planet.service';

describe('PlanetListComponent', () => {
  let component: PlanetListComponent;
  let fixture: ComponentFixture<PlanetListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlanetListComponent],
      providers: [
        PlanetService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PlanetListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
