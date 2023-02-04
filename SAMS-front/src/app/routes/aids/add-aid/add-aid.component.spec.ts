import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAidComponent } from './add-aid.component';

describe('AddAidComponent', () => {
  let component: AddAidComponent;
  let fixture: ComponentFixture<AddAidComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddAidComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
