import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  admin: string

  constructor(private router:Router) { }

  onLogout(){
    localStorage.removeItem('bearer');
    localStorage.removeItem('admin');
    localStorage.removeItem('userId');
    this.router.navigate(['/user/login'])
  }

  ngOnInit() {
    this.admin = localStorage.getItem('admin')
  }

  readLocalStorageValue(key: string): string {
    return localStorage.getItem(key);
  }
}
