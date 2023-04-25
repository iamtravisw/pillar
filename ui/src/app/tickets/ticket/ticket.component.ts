import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/shared/user.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {

  ticket
  commentResults
  id
  user

  constructor(private router: Router, private service: UserService, private activatedRoute: ActivatedRoute, private _location: Location) {
   }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.service.getUserInfo(localStorage.getItem('userId')).subscribe(
      res => {
        this.user = res;
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getTicketById(this.id).subscribe(
      res => {
        this.ticket = res;
        this.service.updateTicketForm(this.ticket)
      },
      err => {
        console.log(err);
      }
    ),
    this.service.getCommentById(this.id).subscribe(
      res => {
        this.commentResults = res;
      },
      err => {
        console.log(err);
      }
    )
  }

  backClicked() {
    this._location.back();
  }

  onSubmit(){
  // make request for user details here
    this.service.addComment(this.id, this.user).subscribe(
      (res:any) => {
        this.service.ticketFormModel.reset();
      },
      err => {
        console.log(err);
      }
    ),
    this.service.updateTicket(this.ticket, localStorage.getItem('userId')).subscribe(
      (res:any) => {
        this.service.ticketFormModel.reset();
        this.backClicked();
      },
      err => {
        console.log(err);
      }
    )
  }
}
