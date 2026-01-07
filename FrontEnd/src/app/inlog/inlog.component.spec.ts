import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InlogComponent } from './inlog.component';

describe('InlogComponent', () => {
  let component: InlogComponent;
  let fixture: ComponentFixture<InlogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InlogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InlogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
