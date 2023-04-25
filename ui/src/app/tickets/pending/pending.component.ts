import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pending',
  templateUrl: './pending.component.html',
  styleUrls: ['./pending.component.css']
})
export class PendingComponent implements OnInit {

  ticketResults

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getTicketsByStatus('Pending').subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }

}
