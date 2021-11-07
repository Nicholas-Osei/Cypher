import { Component, OnInit } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService, UserCredentials } from '../Services/login.service';
import { Buffer } from 'buffer';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  gebruikerCredentials: UserCredentials[];
  isLoggedIn = false;
  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) {
    //this.loginservice.allCredentials.subscribe(c => this.gebruikerCredentials = c);
    //this.loginservice.allCredentials.subscribe(c => this.loginservice.gebruikerCredentials = c);
    console.log(this.gebruikerCredentials[0].base64Credential);
  }
  ngOnInit(): void {
    this.loginservice.allCredentials.subscribe(c => this.gebruikerCredentials = c);
    console.log(this.gebruikerCredentials[0].base64Credential);
  }

  login(): void {
    this.loginservice.loginGoogle();
  }

  checkUserCredential(form: { value: { email: any; password: any } }) {
    console.log(form.value.email);
    const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');
    //get all string from api and check
    // for (let index = 0; index < this.gebruikerCredentials.length; index++) {
    //   if (this.gebruikerCredentials[index].base64Credential === toBAseAuthentication) {
    //     this.router.navigate(['game-screen']);
    //   }
    // }

    for (const x of this.gebruikerCredentials) {
      if (x.base64Credential === toBAseAuthentication) {
        this.router.navigate(['game-screen']);
      }
      console.log(x.base64Credential);
    }

  }
}
