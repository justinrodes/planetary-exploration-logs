import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { DiscoveryDialogComponent } from './discovery-dialog.component';
import { Discovery } from '../discovery.model';

const mockDiscovery: Discovery = {
  id: 1,
  name: 'Test discovery',
  location: null,
  description: null,
  discoveryTypeId: 1,
  missionId: 1
}

describe('DiscoveryDialogComponent', () => {
  let component: DiscoveryDialogComponent;
  let fixture: ComponentFixture<DiscoveryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, DiscoveryDialogComponent],
      providers: [{
        provide: MAT_DIALOG_DATA,
        useValue: { missionId: 1, discovery: mockDiscovery }
      }, {
        provide: MatDialogRef, useValue: {}
      },
      provideHttpClient(),
      provideHttpClientTesting()
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(DiscoveryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
