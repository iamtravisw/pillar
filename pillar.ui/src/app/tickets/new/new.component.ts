import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {

  ticketResults

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getTicketsByStatus('New').subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }
}
