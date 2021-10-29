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

  userData: any = {};
  displayName: any;
  email: any;
  familyName: any;
  givenName: any;
  userId: any;
  imageUrl: any;
  isLoggedIn = false;
  constructor(public googlePlus: GooglePlus, private router: Router, public nav: NavController, public loginservice: LoginService) { }

  login() {
    // const gplusUser = this.googlePlus.login({
    //   webClientId: '646712186754-jlveohr7vqen6rl214hs3bsct3qf5ush.apps.googleusercontent.com',
    //   offline: true,
    //   scopes: 'profile email'
    // })
    //   .then(res => {
    //     console.log(res);
    //     this.displayName = res.displayName;
    //     this.email = res.email;
    //     this.familyName = res.familyName;
    //     this.givenName = res.givenName;
    //     this.userId = res.userId;
    //     this.imageUrl = res.imageUrl;
    //     this.isLoggedIn = true;
    //     this.router.navigate(['game-screen']);
    //   }, result => this.userData = 'Logged in')
    //   .catch(err => console.error(err));
    // ;
    // return this.userData;
    this.loginservice.login();
  }

  // logout() {
  //   this.googlePlus.logout()
  //     .then(res => {
  //       console.log(res);
  //       this.displayName = '';
  //       this.email = '';
  //       this.familyName = '';
  //       this.givenName = '';
  //       this.userId = '';
  //       this.imageUrl = '';

  //       this.isLoggedIn = false;
  //     })
  //     .catch(err => console.error(err));
  // }

}
