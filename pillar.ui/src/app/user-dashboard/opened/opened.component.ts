import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-opened',
  templateUrl: './opened.component.html',
  styleUrls: ['./opened.component.css']
})
export class OpenedComponent implements OnInit {

  ticketResults
  ticketResultsPending
  ticketResultsNew

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getOpenTicketsByUser(localStorage.getItem('userId')).subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getNewTicketsByUser(localStorage.getItem('userId')).subscribe(
      res => {
        this.ticketResultsNew = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getPendingTicketsByUser(localStorage.getItem('userId')).subscribe(
      res => {
        this.ticketResultsPending = res;
      },
      err => {
        console.log(err);
      }
    )
  }
}
