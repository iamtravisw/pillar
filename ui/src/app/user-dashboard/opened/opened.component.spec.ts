import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenedComponent } from './opened.component';

describe('OpenedComponent', () => {
  let component: OpenedComponent;
  let fixture: ComponentFixture<OpenedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
