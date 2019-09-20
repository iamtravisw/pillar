import { Component, OnInit, ResolvedReflectiveFactory } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public service: UserService, private router: Router) { }

  errorMessage;

  ngOnInit() {
    this.service.loginFormModel.reset();
    if(localStorage.getItem('bearer') != null)
    this.router.navigateByUrl('/dashboard');
  }

  onSubmit(){
    this.service.login().subscribe(
      (res:any) => {



         localStorage.setItem('bearer', res.bearer);
         localStorage.setItem('userId', res.userId);
         localStorage.setItem('admin', res.admin);

         if(localStorage.getItem('admin') === 'Y')
         {
           this.router.navigate(['/dashboard']);
          }
          else
          {
          this.router.navigate(['/user-dashboard/new']);
          }
      },
      err => {
        console.log(err);
        this.errorMessage = "403";
        console.log(this.errorMessage)
      }
    );
  }
}
