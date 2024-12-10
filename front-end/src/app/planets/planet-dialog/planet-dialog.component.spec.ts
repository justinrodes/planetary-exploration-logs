import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { PlanetDialogComponent } from './planet-dialog.component';

describe('PlanetDialogComponent', () => {
  let component: PlanetDialogComponent;
  let fixture: ComponentFixture<PlanetDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlanetDialogComponent],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PlanetDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
