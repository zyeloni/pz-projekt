import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAidsComponent } from './list-aids.component';

describe('ListAidsComponent', () => {
  let component: ListAidsComponent;
  let fixture: ComponentFixture<ListAidsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListAidsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAidsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
