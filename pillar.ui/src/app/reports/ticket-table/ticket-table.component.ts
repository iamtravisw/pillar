import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-ticket-table',
  templateUrl: './ticket-table.component.html',
  styleUrls: ['./ticket-table.component.css']
})
export class TicketTableComponent implements OnInit {

  constructor(private service: UserService) { }

  ticketResults
  currentDate = new Date();

  title = "Tickets Table View";
  type = 'Table';
  data = [];
  columnNames = ["Requester", "Organization", "Subject", "Date"];
  options = { 
    alternatingRowStyle:true,
    showRowNumber:true  
  };
  width = 1200;
  height = 300;

  ngOnInit() {

    this.service.getTicketsUserDashboard().subscribe(
      res => {
        this.ticketResults = res;

        for (var ticket of this.ticketResults)
        {
          this.data.push([ticket.user.requester, ticket.user.organization, ticket.subject, new Date(ticket.openDate)]);
        }
      },
      err => {
        console.log(err);
      }
    )
    }
  }