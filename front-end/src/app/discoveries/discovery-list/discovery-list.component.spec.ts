import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { DiscoveryListComponent } from './discovery-list.component';
import { DiscoveryService } from '../discovery.service';

describe('DiscoveryListComponent', () => {
  let component: DiscoveryListComponent;
  let fixture: ComponentFixture<DiscoveryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiscoveryListComponent],
      providers: [
        DiscoveryService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(DiscoveryListComponent);
    component = fixture.componentInstance;
    component.missionId.set(1);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
