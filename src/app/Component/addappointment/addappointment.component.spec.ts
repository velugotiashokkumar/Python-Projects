import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddappointmentComponent } from './addappointment.component';

describe('AddappointmentComponent', () => {
  let component: AddappointmentComponent;
  let fixture: ComponentFixture<AddappointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddappointmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
