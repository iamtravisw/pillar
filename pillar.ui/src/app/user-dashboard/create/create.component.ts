import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  user

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.createTicketFormModel.reset();
    this.service.getUserInfo(localStorage.getItem('userId')).subscribe(
      res => {
        this.user = res;
      },
      err => {
        console.log(err);
      }
    )
  }

  onSubmit(){
    // make request for user details here
      this.service.createTicket(this.user).subscribe(
        (res:any) => {
          this.router.navigate(['/user-dashboard/opened']);
        },
        err => {
          console.log(err);
        }
      )
  }
}
