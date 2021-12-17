/* eslint-disable quote-props */
import { Injectable } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Platform } from '@ionic/angular';
import { GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';
import { PlayerService } from './player.service';

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
  stayLoggedIn: boolean;
  // d = device.cordova;
  // gebruikerCredentials = [];
  players: any;
  gebruikerCredentials: RootObject;
  // eslint-disable-next-line max-len
  authToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdXBlcmFkbWluIiwianRpIjoiOGVhMDYxMTQtZTcxYi00MTljLTlhYmMtYWFiMjgwMzcxNzRmIiwiZW1haWwiOiJzdXBlcmFkbWluQGdtYWlsLmNvbSIsInVpZCI6IjJlN2FkMzVhLTliYTctNGQzOS04ZGQxLTc3ZmI0N2U0YzVkZiIsImZpcnN0X25hbWUiOiJNdWtlc2giLCJsYXN0X25hbWUiOiJNdXJ1Z2FuIiwiZnVsbF9uYW1lIjoiTXVrZXNoIE11cnVnYW4iLCJpcCI6IjAuMC4wLjEiLCJyb2xlcyI6WyJBZG1pbiIsIk1vZGVyYXRvciIsIkJhc2ljIiwiU3VwZXJBZG1pbiJdLCJuYmYiOjE2MzY1NDQ2NjUsImV4cCI6MTYzNjU0ODI2NSwiaXNzIjoiQ3lwaGVyLkFwaSIsImF1ZCI6IkN5cGhlci5BcGkuVXNlciJ9.fdBujUAYnruEO4uNhW6j7Vsb2BD6KC-ZCz6hNxrDPgQ';

  constructor(public googlePlus: GooglePlus, private router: Router, private http: HttpClient,
    public platform: Platform, private socialAuthService: SocialAuthService) { }


  allCredentials(): Observable<RootObject> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      // eslint-disable-next-line quote-props
      // eslint-disable-next-line @typescript-eslint/naming-convention
      'Authorization': `Bearer ${this.authToken}`
    });
    //return this.http.get<RootObject>('https://api.genderize.io/?name=luc');
    return this.http.get<RootObject>('https://localhost:5001/api/v1/UserCredential?pageSize=100500',
      { headers: httpHeaders });
  }

  postCredential(credential): Observable<RootObject> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      // eslint-disable-next-line quote-props
      // eslint-disable-next-line @typescript-eslint/naming-convention
      'Authorization': `Bearer ${this.authToken}`
    });
    //return this.http.get<RootObject>('https://api.genderize.io/?name=luc');
    return this.http.post<RootObject>('https://localhost:5001/api/v1/UserCredential', credential, { headers: httpHeaders });
  }

  async getToken(gegevens) {
    return this.http.post<Tokens>('https://localhost:5001/api/identity/token', gegevens).toPromise();
  }

  login() {
    if (!this.platform.ready) {
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
    else {
      console.log('web');
      this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(res => {
        console.log('logged in');
        this.displayName = res.name;
        this.isLoggedIn = true;
        localStorage.setItem('token', this.authToken);
        localStorage.setItem('Isloggedin', JSON.stringify(this.isLoggedIn));
        localStorage.setItem('Displayname', JSON.stringify(this.displayName));
        this.router.navigate(['game-screen']);
        // this.playerservice.getAllPlayers().subscribe(p => this.players = p.data);
        // this.players.data.forEach(element => {
        //   console.log(element);
        // });

        // const credentials =
        // {
        //   name: res.name,
        //   isAdmin: false,
        //   inventory: {
        //     items: []
        //   },
        //   messages: [],
        //   playerLobbies: []

        // };
        // this.playerservice.postPlayer(credentials).subscribe(a => console.log('Player Added'));
      });
    }
  }


  logout() {
    if (!this.platform.ready) {
      this.googlePlus.logout()
        .then(res => {
          console.log(res);
          this.displayName = '';
          this.email = '';
          this.familyName = '';
          this.givenName = '';
          this.userId = '';
          this.imageUrl = '';

          // this.isLoggedIn = false;
          // this.router.navigate(['home']);
        })
        .catch(err => console.error(err));
    }
    else {
      this.socialAuthService.signOut();
    }
    this.isLoggedIn = false;
    localStorage.clear();
    this.router.navigate(['home']);
  }


}
export interface UserCredentials {
  id: number;
  base64Credential: string;
}

export interface RootObject {
  data: UserCredentials[];
  page: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  failed: boolean;
  message?: any;
  succeeded: boolean;
}
export interface Tokens {
  data: any;

}
