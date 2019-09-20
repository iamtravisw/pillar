import { Component, OnInit, wtfCreateScope } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-ticket-by-status',
  templateUrl: './ticket-by-status.component.html',
  styleUrls: ['./ticket-by-status.component.css']
})
export class TicketByStatusComponent implements OnInit {

  constructor(private service: UserService) { }

  title = 'Tickets by Status';
  type = 'PieChart';
  data = [];
  columnNames = ['Ticket', 'Percentage'];
  options = {};
  width = 1200;
  height = 600;

  ngOnInit() {
  
    this.service.getNewTickets().subscribe(
      res => {
        this.data.push(['New', +res])
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getOpenTickets().subscribe(
      res => {
        this.data.push(['Open', +res])
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getPendingTickets().subscribe(
      res => {
       this.data.push(['Pending', +res])
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getSolvedTickets().subscribe(
      res => {
        this.data.push(['Solved', +res])
      },
      err => {
        console.log(err);
      }
    )
  }
}