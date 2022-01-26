import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  inventory: any;
  players: Player;
  friends: any;
  playerId: any;
  lobbyNaam: string;
  inventoryId: number;

  constructor(private http: HttpClient, public loginservice: LoginService) {

  }

  reloadPage() {
    window.location.reload();
  }

  getAllPlayers(): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.get<Player>('https://cypher-web-app.azurewebsites.net/api/v1/player?pageSize=500',
      { headers: httpHeaders });
  }

  getAllQuestions(): Observable<Question> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.get<Question>('https://cypher-web-app.azurewebsites.net/api/v1/question?pageSize=500',
      { headers: httpHeaders });
  }

  getPlayerById(id): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.get<Player>('https://cypher-web-app.azurewebsites.net/api/v1/player/' + id, { headers: httpHeaders });
  }

  searchForFriends(name): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.get<Player>('https://cypher-web-app.azurewebsites.net/api/v1/player?playername=' + name, { headers: httpHeaders });
  }

  addToPlayerFriends(id, gegevens: any): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.put<Player>('https://cypher-web-app.azurewebsites.net/api/v1/Player/' + id + '/friends', gegevens, { headers: httpHeaders });
  }

  deleteFriend(playerid, friendid): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.delete<Player>('https://cypher-web-app.azurewebsites.net/api/v1/Player/' + playerid + '/friends/' + friendid, { headers: httpHeaders });
  }

  postPlayer(gegevens): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.post<Player>('https://cypher-web-app.azurewebsites.net/api/v1/player?pageSize=500', gegevens, { headers: httpHeaders });
  }

  updatePlayer(gegevens, id: number): Observable<Player> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.put<Player>('https://cypher-web-app.azurewebsites.net/api/v1/player/' + id + '?pageSize=500', gegevens, { headers: httpHeaders });
  }
  
  updateInventoryItems(id: number, gegevens: any): Observable<Inventory> {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.put<Inventory>('https://cypher-web-app.azurewebsites.net/api/v1/inventory/' + id + '?pageSize=500', gegevens, { headers: httpHeaders });
  }

  createNewItem(gegevens): Observable<Item>{
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.post<Item>('https://cypher-web-app.azurewebsites.net/api/v1/item/' + gegevens, { headers: httpHeaders });
  }

  deleteInventoryItem(id: number) {
    const httpHeaders = new HttpHeaders({
      'content-type': 'application/json',
      'Authorization': `Bearer ${this.loginservice.authToken}`
    });
    return this.http.delete<Inventory>('https://cypher-web-app.azurewebsites.net/api/v1/item/' + id + '?pageSize=500', { headers: httpHeaders });
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
  friends: any[];
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
export interface Questions {
  id: number;
  question: string;
  answer: string;
  type: string;
  location: string;
  option1?: any;
  option2?: any;
}
export interface Question {
  data: Questions[];
  page: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  failed: boolean;
  message?: any;
  succeeded: boolean;
}
