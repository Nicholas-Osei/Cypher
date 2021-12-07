import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './Services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {

  constructor(private router: Router, public loginservice: LoginService) {
    if (localStorage.getItem('token')) {
      console.log('yeah');
      this.loginservice.authToken = localStorage.getItem('token');
      this.loginservice.displayName = JSON.parse(localStorage.getItem('Displayname'));
      this.loginservice.isLoggedIn = JSON.parse(localStorage.getItem('Isloggedin'));
    }
    else {
      // this.router.navigate(['game-screen']);
    }


  }
}
