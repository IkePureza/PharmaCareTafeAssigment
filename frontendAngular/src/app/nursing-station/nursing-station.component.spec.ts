import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NursingStationComponent } from './nursing-station.component';

describe('NursingStationComponent', () => {
  let component: NursingStationComponent;
  let fixture: ComponentFixture<NursingStationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NursingStationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NursingStationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
