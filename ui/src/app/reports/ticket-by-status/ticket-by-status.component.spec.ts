import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketByStatusComponent } from './ticket-by-status.component';

describe('TicketByStatusComponent', () => {
  let component: TicketByStatusComponent;
  let fixture: ComponentFixture<TicketByStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketByStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketByStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
