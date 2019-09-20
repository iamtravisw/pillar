import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SolvedComponent } from './solved.component';

describe('SolvedComponent', () => {
  let component: SolvedComponent;
  let fixture: ComponentFixture<SolvedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SolvedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SolvedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
