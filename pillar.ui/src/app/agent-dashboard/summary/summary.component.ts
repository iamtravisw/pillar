import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {

  newCount;
  pendingCount;
  openCount;
  solvedCount;

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getNewTickets().subscribe(
      res => {
        this.newCount = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getPendingTickets().subscribe(
      res => {
        this.pendingCount = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getOpenTickets().subscribe(
      res => {
        this.openCount = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getSolvedTickets().subscribe(
      res => {
        this.solvedCount = res;
      },
      err => {
        console.log(err);
      }
    )

  }
}
