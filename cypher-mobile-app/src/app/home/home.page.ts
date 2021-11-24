import { Component, OnInit } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService, RootObject, UserCredentials } from '../Services/login.service';
import { Buffer } from 'buffer';
import { render } from 'creditcardpayments/creditCardPayments';


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
  mybalance = 0;
  moneyToPay = 10;

  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService) {

    // render({
    //   id: '#myPaypalButtons',
    //   currency: 'EUR',
    //   value: this.moneyToPay.toString(),
    //   onApprove: (details) => {
    //     alert('Transaction succesfull');
    //     this.mybalance += this.moneyToPay;
    //   }

    // });

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
    this.getToken();
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
