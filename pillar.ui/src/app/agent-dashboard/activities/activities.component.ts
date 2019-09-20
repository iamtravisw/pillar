import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {

  ticketResults;
  
  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
  
    this.service.getTicketsUserDashboard().subscribe(
      res => {
        this.ticketResults = res
      },
      err => {
        console.log(err);
      }
    )

  }
}
