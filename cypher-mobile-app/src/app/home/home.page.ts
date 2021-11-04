import { Component } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService } from '../Services/login.service';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  isLoggedIn = false;
  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) { }

  login() {
    this.loginservice.login();
  }
}
