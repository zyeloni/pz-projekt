import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarAidsComponent } from './calendar-aids.component';

describe('CalendarAidsComponent', () => {
  let component: CalendarAidsComponent;
  let fixture: ComponentFixture<CalendarAidsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalendarAidsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarAidsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
