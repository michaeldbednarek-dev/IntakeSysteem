import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplatePaginaComponent } from './template-pagina.component';

describe('TemplatePaginaComponent', () => {
  let component: TemplatePaginaComponent;
  let fixture: ComponentFixture<TemplatePaginaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemplatePaginaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TemplatePaginaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
