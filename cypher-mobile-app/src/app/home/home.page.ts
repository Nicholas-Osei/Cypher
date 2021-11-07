import { Component, OnInit } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService, RootObject, UserCredentials } from '../Services/login.service';
import { Buffer } from 'buffer';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  gebruikerCredentials: RootObject;
  isLoggedIn = false;
  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) {
    //this.loginservice.allCredentials.subscribe(c => this.gebruikerCredentials = c);
    //this.loginservice.allCredentials.subscribe(c => this.loginservice.gebruikerCredentials = c);
    console.log(this.gebruikerCredentials);
  }
  ngOnInit(): void {
    this.loginservice.allCredentials.subscribe(c => this.gebruikerCredentials = c);
    console.log(this.gebruikerCredentials);
  }

  login(): void {
    this.loginservice.loginGoogle();
  }

  checkUserCredential(form: { value: { email: any; password: any } }) {
    console.log(form.value.email);
    const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');

    for (const x of this.gebruikerCredentials.data) {
      if (x.base64Credential === toBAseAuthentication) {
        this.loginservice.isLoggedIn = true;
        this.router.navigate(['game-screen']);
      }
      // console.log(x.base64Credential);
      // console.log(toBAseAuthentication);
    }
    this.loginservice.displayName = form.value.email;

  }
}
