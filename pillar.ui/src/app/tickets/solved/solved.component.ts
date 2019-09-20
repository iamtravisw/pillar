import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-solved',
  templateUrl: './solved.component.html',
  styleUrls: ['./solved.component.css']
})
export class SolvedComponent implements OnInit {

  ticketResults

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getTicketsByStatus('Solved').subscribe(
      res => {
        this.ticketResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }
}
