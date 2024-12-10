import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MissionListComponent } from './mission-list.component';
import { MissionService } from '../mission.service';

describe('MissionListComponent', () => {
  let component: MissionListComponent;
  let fixture: ComponentFixture<MissionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MissionListComponent],
      providers: [
        MissionService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MissionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
