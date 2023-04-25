import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketByCustomerComponent } from './ticket-by-customer.component';

describe('TicketByCustomerComponent', () => {
  let component: TicketByCustomerComponent;
  let fixture: ComponentFixture<TicketByCustomerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketByCustomerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketByCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
