import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IntakePlannenComponent } from './intake-plannen.component';

describe('IntakePlannenComponent', () => {
  let component: IntakePlannenComponent;
  let fixture: ComponentFixture<IntakePlannenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IntakePlannenComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IntakePlannenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
