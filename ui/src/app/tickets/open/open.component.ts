import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-open',
  templateUrl: './open.component.html',
  styleUrls: ['./open.component.css']
})
export class OpenComponent implements OnInit {

  ticketResults

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getTicketsByStatus('Open').subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }
}
