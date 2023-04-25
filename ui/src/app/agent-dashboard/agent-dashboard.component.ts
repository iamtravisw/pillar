import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-agent-dashboard',
  templateUrl: './agent-dashboard.component.html',
  styleUrls: ['./agent-dashboard.component.css']
})
export class AgentDashboardComponent implements OnInit {

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
