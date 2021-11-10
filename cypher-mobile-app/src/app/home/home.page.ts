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
export class HomePage {

  gebruikerCredentials: RootObject;
  isLoggedIn = false;
  toRegister = false;
  passwordcheck = '';
  teller = 0;
  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) {
    //this.loginservice.allCredentials.subscribe(c => this.gebruikerCredentials = c);
    //this.loginservice.allCredentials.subscribe(c => this.loginservice.gebruikerCredentials = c);
    //console.log(this.gebruikerCredentials);
    // const newtoken =
    // {
    //   email: 'superadmin@gmail.com',
    //   password: '123Pa$$word!'
    // };
    this.getToken();
    // this.loginservice.getToken(newtoken).then(t => { console.log('Got it', this.loginservice.authToken = t.data.jwToken); });
    // this.loginservice.allCredentials().subscribe(c => { this.gebruikerCredentials = c; console.log('eeeeeh'); });
    //console.log('work:', this.gebruikerCredentials);
  }

  async getToken() {
    const newtoken =
    {
      email: 'superadmin@gmail.com',
      password: '123Pa$$word!'
    };
    await this.loginservice.getToken(newtoken).then(t => { console.log('Got it', this.loginservice.authToken = t.data.jwToken); });
    return this.loginservice.allCredentials().subscribe(c => { this.gebruikerCredentials = c; console.log('Got all credentials'); });
  }
  makeRegisterTrue(): void {
    this.toRegister = true;

  }

  makeRegisterFalse(): void {
    this.toRegister = false;

  }



  login(): void {
    this.loginservice.login();
  }


  checkUserCredential(form: { value: { email: any; password: any } }) {
    console.log(form.value.email);
    const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');
    console.log(toBAseAuthentication);
    for (const x of this.gebruikerCredentials.data) {
      if (x.base64Credential === toBAseAuthentication) {
        this.loginservice.isLoggedIn = true;
        console.log('check this', x.base64Credential);
        console.log(toBAseAuthentication);
        this.loginservice.displayName = form.value.email;
        this.router.navigate(['game-screen']);
      }

    }
    // if (this.loginservice.isLoggedIn) {
    //   this.router.navigate(['game-screen']);
    // }


    //this works with different not localhost api
    // this.loginservice.isLoggedIn = true;
    // this.router.navigate(['game-screen']);
    // this.loginservice.displayName = form.value.email;
    // //this.loginservice.familyName = this.gebruikerCredentials.activity;
    // this.loginservice.familyName = this.gebruikerCredentials.name;
  }
  registerUser(form: { value: { email: any; password: any; confirmpassword: any } }) {
    if (form.value.confirmpassword === form.value.password) {
      const toBAseAuthentication = Buffer.from(form.value.email + form.value.confirmpassword).toString('base64');
      const newCredential = {
        base64Credential: toBAseAuthentication
      };
      for (const x of this.gebruikerCredentials.data) {
        this.teller++;
        if (x.base64Credential === toBAseAuthentication) {
          this.passwordcheck = 'User Already Exist! Please Go back and Log in';
        }
        else if ((this.gebruikerCredentials.data.length - 1) === this.teller) {
          console.log('not found');
          this.loginservice.postCredential(newCredential).subscribe(d => { console.log('Added'); this.toRegister = false; });
        }
      }

    }
    else {
      this.passwordcheck = 'Password and Confirm password should be the same! ';
    }


  }

}
