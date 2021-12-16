import { Component, OnInit } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { NavController } from '@ionic/angular';
import { LoginService, RootObject, UserCredentials } from '../Services/login.service';
import { Buffer } from 'buffer';
import { render } from 'creditcardpayments/creditCardPayments';
import { PlayerService } from '../Services/player.service';
import { ThisReceiver } from '@angular/compiler';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  // gebruikerCredentials: RootObject;

  isLoggedIn = false;
  toRegister = false;
  passwordcheck = '';
  teller = 0;
  mybalance = 0;
  moneyToPay = 10;

  constructor(public googlePlus: GooglePlus, private router: Router,
    public nav: NavController, public loginservice: LoginService, public playerservice: PlayerService) {

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

  ngOnInit() {
    this.getToken();
  }

  async getToken() {
    const newtoken =
    {
      email: 'superadmin@gmail.com',
      password: '123Pa$$word!'
    };
    await this.loginservice.getToken(newtoken).then(t => { console.log('Got it', this.loginservice.authToken = t.data.jwToken); });
    // eslint-disable-next-line max-len
    return this.loginservice.allCredentials().subscribe(c => { this.loginservice.gebruikerCredentials = c; console.log('Got all credentials'); });
  }
  registerBoolean(): void {
    // this.toRegister = true;
    this.toRegister = !this.toRegister;
  }







  login(): void {
    this.loginservice.login();
  }


  checkUserCredential(form: { value: { email: any; password: any } }) {
    console.log(form.value.email);
    // this.loginservice.allCredentials().subscribe(c => { this.gebruikerCredentials = c; console.log('Refresh'); });
    const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');
    console.log(toBAseAuthentication);
    console.log(this.loginservice.gebruikerCredentials);
    for (const x of this.loginservice.gebruikerCredentials.data) {
      console.log(x.base64Credential, toBAseAuthentication);
      if (x.base64Credential === toBAseAuthentication) {
        this.loginservice.isLoggedIn = true;
        console.log('check this', x.base64Credential);
        console.log(toBAseAuthentication);
        this.loginservice.displayName = form.value.email;
        localStorage.setItem('token', this.loginservice.authToken);
        localStorage.setItem('Isloggedin', JSON.stringify(this.loginservice.isLoggedIn));
        localStorage.setItem('Displayname', JSON.stringify(this.loginservice.displayName));
        this.router.navigate(['game-screen']);

      }
      console.log('doesnt exist');

    }
    this.loginservice.allCredentials().subscribe(c => { this.loginservice.gebruikerCredentials = c; });

  }
  registerUser(form: { value: { email: any; password: any; confirmpassword: any } }) {
    console.log(this.loginservice.gebruikerCredentials.data);
    if (form.value.confirmpassword === form.value.password) {
      const toBAseAuthentication = Buffer.from(form.value.email + form.value.password).toString('base64');
      const newCredential = {
        base64Credential: toBAseAuthentication
      };
      if (this.loginservice.gebruikerCredentials.data.length === 0) {
        console.log('null');
        // eslint-disable-next-line max-len
        this.loginservice.postCredential(newCredential).subscribe(d => { console.log('Added', toBAseAuthentication); this.toRegister = false; this.teller = 0; });
        this.ngOnInit();
      }
      else {
        for (const x of this.loginservice.gebruikerCredentials.data) {
          this.teller++;
          console.log(this.loginservice.gebruikerCredentials.data.length - 1, this.teller);
          console.log(this.loginservice.gebruikerCredentials.data.length);
          if (x.base64Credential === toBAseAuthentication) {
            this.passwordcheck = 'User Already Exist! Please Go back and Log in';
          }
          // eslint-disable-next-line max-len
          else if ((this.loginservice.gebruikerCredentials.data.length - 1 === this.teller) || this.loginservice.gebruikerCredentials.data.length === 1) {
            console.log('not found');
            console.log(this.loginservice.gebruikerCredentials.data);
            // eslint-disable-next-line max-len
            this.loginservice.postCredential(newCredential).subscribe(d => { console.log('Added', toBAseAuthentication); this.toRegister = false; this.teller = 0; });
            const credentials =
            {
              name: form.value.email,
              isAdmin: false,
              inventory: {
                items: []
              },
              messages: [],
              playerLobbies: []

            };
            this.playerservice.postPlayer(credentials).subscribe(a => console.log('Player Added'));
            this.ngOnInit();
          }
        }

      }

    }
    else {
      this.passwordcheck = 'Password and Confirm password should be the same! ';
    }


  }





}
