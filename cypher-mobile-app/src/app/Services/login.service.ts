import { Injectable } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  displayName: any;
  email: any;
  familyName: any;
  givenName: any;
  userId: any;
  imageUrl: any;
  isLoggedIn = false;
  userData: any = {};
  constructor(public googlePlus: GooglePlus, private router: Router) { }


  // get allCredentials(): Observable<Credentials[]> {
  //   return this.http.get<Credentials[]>('https://localhost:5001/api/v1/orders');
  // }

  loginGoogle() {
    const gplusUser = this.googlePlus.login({
      webClientId: '646712186754-jlveohr7vqen6rl214hs3bsct3qf5ush.apps.googleusercontent.com',
      offline: true,
      scopes: 'profile email'
    })
      .then(res => {
        console.log(res);
        this.displayName = res.displayName;
        this.email = res.email;
        this.familyName = res.familyName;
        this.givenName = res.givenName;
        this.userId = res.userId;
        this.imageUrl = res.imageUrl;
        this.isLoggedIn = true;
        this.router.navigate(['game-screen']);
      }, result => this.userData = 'Logged in')
      .catch(err => console.error(err));
    ;
    return this.userData;
  }

  logoutGoogle() {
    this.googlePlus.logout()
      .then(res => {
        console.log(res);
        this.displayName = '';
        this.email = '';
        this.familyName = '';
        this.givenName = '';
        this.userId = '';
        this.imageUrl = '';

        this.isLoggedIn = false;
        this.router.navigate(['home']);
      })
      .catch(err => console.error(err));
  }


}
