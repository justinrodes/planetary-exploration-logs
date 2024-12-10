import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { PlanetPanelComponent } from './planet-panel.component';

describe('PlanetPanelComponent', () => {
  let component: PlanetPanelComponent;
  let fixture: ComponentFixture<PlanetPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, PlanetPanelComponent],
      providers: [provideRouter([])]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PlanetPanelComponent);
    component = fixture.componentInstance;
    component.planet.set({
      id: 1,
      name: 'Test planet',
      type: 'Test type',
      climate: null,
      population: null,
      terrain: null
    });
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
