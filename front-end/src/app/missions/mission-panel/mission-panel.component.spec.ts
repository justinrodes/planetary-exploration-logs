import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { MissionPanelComponent } from './mission-panel.component';

describe('MissionPanelComponent', () => {
  let component: MissionPanelComponent;
  let fixture: ComponentFixture<MissionPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, MissionPanelComponent],
      providers: [provideRouter([])]
    }).compileComponents();

    fixture = TestBed.createComponent(MissionPanelComponent);
    component = fixture.componentInstance;
    component.mission.set({
      id: 1,
      name: 'Test mission',
      date: '2025-01-01',
      planetId: 1,
      description: null
    });
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
