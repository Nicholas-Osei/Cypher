/* eslint-disable quote-props */
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {



  constructor(private http: HttpClient, public loginservice: LoginService) {
    // const newtoken =
    // {
    //   email: 'superadmin@gmail.com',
    //   password: '123Pa$$word!'
    // };

  }

  getAllPlayers(): Observable<Player> {

    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      // eslint-disable-next-line quote-props
      // eslint-disable-next-line @typescript-eslint/naming-convention
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.get<Player>('https://localhost:5001/api/v1/player?pageSize=500',
      { headers: httpHeaders });
  }

  postPlayer(gegevens): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      // eslint-disable-next-line quote-props
      // eslint-disable-next-line @typescript-eslint/naming-convention
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.post<Player>('https://localhost:5001/api/v1/player?pageSize=500', gegevens, { headers: httpHeaders });
  }
}



export interface Item {
  name: string;
  itemType: string;
  id: number;
  createdBy: string;
  createdOn: Date;
  lastModifiedBy?: any;
  lastModifiedOn?: any;
}

export interface Inventory {
  items: Item[];
  id: number;
}

export interface Speler {
  id: number;
  check: number;
  name: string;
  isAdmin: boolean;
  inventory: Inventory;
  messages: any[];
  playerLobbies: any[];
}
export interface Player {
  data: Speler[];
  page: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  failed: boolean;
  message?: any;
  succeeded: boolean;
}
