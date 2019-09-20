import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-closed',
  templateUrl: './closed.component.html',
  styleUrls: ['./closed.component.css']
})
export class ClosedComponent implements OnInit {

  ticketResults

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getSolvedTicketsByUser(localStorage.getItem('userId')).subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }

}
