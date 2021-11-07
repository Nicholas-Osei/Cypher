/* eslint-disable quote-props */
import { Injectable } from '@angular/core';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { Router, RouterLink } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

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
  gebruikerCredentials = [];
  // eslint-disable-next-line max-len
  authToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdXBlcmFkbWluIiwianRpIjoiM2FlYzVkZWMtOThjMS00NWQ3LTlmN2EtYjM2YjE5YzhkODk0IiwiZW1haWwiOiJzdXBlcmFkbWluQGdtYWlsLmNvbSIsInVpZCI6IjJlN2FkMzVhLTliYTctNGQzOS04ZGQxLTc3ZmI0N2U0YzVkZiIsImZpcnN0X25hbWUiOiJNdWtlc2giLCJsYXN0X25hbWUiOiJNdXJ1Z2FuIiwiZnVsbF9uYW1lIjoiTXVrZXNoIE11cnVnYW4iLCJpcCI6IjAuMC4wLjEiLCJyb2xlcyI6WyJBZG1pbiIsIk1vZGVyYXRvciIsIkJhc2ljIiwiU3VwZXJBZG1pbiJdLCJuYmYiOjE2MzYzMDQwNjksImV4cCI6MTYzNjMwNzY2OSwiaXNzIjoiQ3lwaGVyLkFwaSIsImF1ZCI6IkN5cGhlci5BcGkuVXNlciJ9.UJh6W9QK69BcRH2KKazYaXBl_4Pl2gfDWGH8uRpVR0Q';

  constructor(public googlePlus: GooglePlus, private router: Router, private http: HttpClient) { }


  get allCredentials(): Observable<RootObject> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      // eslint-disable-next-line quote-props
      // eslint-disable-next-line @typescript-eslint/naming-convention
      'Authorization': `Bearer ${this.authToken}`
    });
    return this.http.get<RootObject>('https://localhost:5001/api/v1/UserCredential', { headers: httpHeaders });
  }

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

        // this.isLoggedIn = false;
        // this.router.navigate(['home']);
      })
      .catch(err => console.error(err));
    this.isLoggedIn = false;
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
