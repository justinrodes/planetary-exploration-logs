import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { MissionDialogComponent } from './mission-dialog.component';

describe('MissionDialogComponent', () => {
  let component: MissionDialogComponent;
  let fixture: ComponentFixture<MissionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MissionDialogComponent],
      providers: [
        provideRouter([]),
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MissionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
