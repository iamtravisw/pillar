import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-ticket-by-customer',
  templateUrl: './ticket-by-customer.component.html',
  styleUrls: ['./ticket-by-customer.component.css']
})
export class TicketByCustomerComponent implements OnInit {

  constructor(private service: UserService) { }

  ticketResults
  orgs = [];

  title = 'Tickets by Customer';
   type = 'ColumnChart';
   data = [];
   columnNames = ['Company', '# Tickets'];
   options = {};
   width = 1200;
   height = 600;


  ngOnInit() {

    this.service.getTicketsUserDashboard().subscribe(
      res => {
        this.ticketResults = res;

        for (var ticket of this.ticketResults)
        {
          this.orgs.push(ticket.user.organization)
        }

        var counts = {};
        this.orgs.forEach(function(x) {
          counts[x] = (counts[x] || 0) + 1;
        });
        for (const key of Object.keys(counts)) {
          const val = counts[key];
          this.data.push([key, +val]);
          
        }
      },
      err => {
        console.log(err);
      }
    )
  }
}
