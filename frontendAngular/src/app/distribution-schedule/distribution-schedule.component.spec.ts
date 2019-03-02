import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DistributionScheduleComponent } from './distribution-schedule.component';

describe('DistributionScheduleComponent', () => {
  let component: DistributionScheduleComponent;
  let fixture: ComponentFixture<DistributionScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DistributionScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DistributionScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
