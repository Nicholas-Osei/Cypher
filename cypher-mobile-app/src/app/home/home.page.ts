import { Component } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService } from '../Services/login.service';
import { Buffer } from 'buffer';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  UserCredentials: Credentials[];
  isLoggedIn = false;
  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) {
    this.loginservice.allCredentials.subscribe(c => this.UserCredentials = c);

  }



  login() {
    this.loginservice.loginGoogle();
  }

  checkUserCredential(form: { value: { email: any; password: any } }) {
    console.log(form.value.email);
    const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');
    //get all string from api and check
    for (let index = 0; index < UserCredentials.length; index++) {
      if (this.UserCredentials[index].credentials === toBAseAuthentication) {
        this.router.navigate(['game-screen']);
      }
    }

  }
}
