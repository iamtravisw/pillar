import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService,  private router: Router) { }

  ngOnInit() {
    this.service.registerFormModel.reset();
    if(localStorage.getItem('bearer') != null)
    this.router.navigateByUrl('/dashboard');
  }

  onSubmit(){
    this.service.register().subscribe(
      (res:any) => {
        this.service.registerFormModel.reset();
        this.router.navigateByUrl('/user/login');
      },
      err => {
        console.log(err);
      }
    );
  }

}
