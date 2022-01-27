import { ThisReceiver } from '@angular/compiler';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { Inventory, Player, ApiService } from '../Services/api.service';




@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.page.html',
  styleUrls: ['./game-screen.page.scss'],
})
export class GameScreenPage implements OnInit, OnDestroy {

  // players: Player;
  // inventory: any;
  showPage: any;
  lobbies: any;
  friends: any;
  messages: any;
  playerbyId: any;
  id: number;
  playerSearchResults: any;
  playername = '';

  time = new Date();
  rxTime = new Date();
  intervalId;
  tabel = Array(5);
  titel = '';
  teller = 0;
  notViaGoogle = false;
  randomNumber = 0;
  imageUrl = '';
  inLobby = 0;

  constructor(public loginservice: LoginService, public api: ApiService, public router: Router) {
    this.imageUrl = './assets/icon/person' + 1 + '.jpeg';
    //this.titel = 'Welcome, ' + loginservice.displayName + '!';
  }
  ngOnInit(): void {
    this.api.getAllPlayers().subscribe(p => {
      this.api.players = p; console
        .log('Got all players');
      console.log(this.api.players.data[0].name);
      this.getUserItems();
      // this.getPlayerbyId();
      console.log(this.api.inventory);
    });

    this.intervalId = setInterval(() => {
      this.time = new Date();
    }, 1000);
    this.api.getAllLobbies().subscribe(l => {
      this.lobbies = l.data;
      console.log('Got all lobbies');
      this.aantalLobbies();
    });
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }

  aantalLobbies() {
    this.lobbies.forEach(element => {
      element.players.forEach(p => {
        if (this.loginservice.displayName === p.name) {
          this.inLobby++;
        }
      });
    });
    console.log(this.inLobby);
  }
  goToMap() {
    this.router.navigate(['map-screen'],
    ).then(() => { window.location.reload(); });
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string) {
    console.log('Called open ' + page);
    this.router.navigate([page]).then(() => window.location.reload());
  }

  async getUserItems() {
    console.log(this.loginservice.displayName);
    this.api.players.data.forEach(m => {
      this.teller++;
      // console.log(this.loginservice.displayName);
      console.log(this.api.players.data.length, this.teller);
      if (m.name === this.loginservice.displayName) {
        //
        // this.speler.inventory = m.inventory.items;
        // this.messages = m.messages;
        // this.lobbies = m.playerLobbies;
        // this.id = m.id;
        this.api.playerId = m.id;
        console.log(m.name);
        localStorage.setItem('Id', JSON.stringify(m.id));
        this.notViaGoogle = true;
      }
    });
    console.log(this.notViaGoogle);
    if (!this.notViaGoogle) {
      console.log(this.notViaGoogle);
      const credentials =
      {
        name: this.loginservice.displayName,
        isAdmin: false,
        inventory: {
          items: []
        },
        messages: [],
        playerLobbies: []

      };
      this.api.postPlayer(credentials).subscribe(a => { console.log('Player Added'); window.location.reload(); });
    }
    this.getPlayerbyId();
  }

  getPlayerbyId() {
    this.api.getPlayerById(this.api.playerId).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.friends);
        this.friends = this.playerbyId.data.friends;
        this.api.inventory = this.playerbyId.data.inventory;
        // this.speler.friends = this.playerbyId.data.friends;
        // this.speler.messages = m.messages;
        // this.lobbies = m.playerLobbies;
      });

  }

  setShowPageToFriends() {
    this.showPage = 'friends';
  }

  search(name?: any) {
    console.log(this.playername);
    // console.log(this.)
    this.api.searchForFriends(this.playername).
      subscribe(s => { this.playerSearchResults = s.data; console.log(this.playerSearchResults); });
  }


  deleteFriend(id: number) {
    this.api.deleteFriend(this.api.playerId, id).subscribe(d => { console.log('deleted'); this.ngOnInit(); });
  }

  addFriend(id: number) {
    console.log(this.api.playerId);

    const newFriend = {
      playerId: this.api.playerId,
      friendId: id
    };
    // eslint-disable-next-line max-len
    this.api.addToPlayerFriends(this.id, newFriend).subscribe(f => { console.log('Friend Added'); this.generateRandomNumber(); this.ngOnInit(); });
  }




  openNav() {
    document.getElementById('mySidenav').style.width = '250px';
  }

  closeNav() {
    document.getElementById('mySidenav').style.width = '0';
  }

  generateRandomNumber() {
    // eslint-disable-next-line prefer-const
    this.randomNumber = Math.floor((Math.random() * 3) + 1);
    this.imageUrl = './assets/icon/person' + this.randomNumber + '.jpeg';
    console.log(this.randomNumber);
    return this.randomNumber;
  }

  renderPage(page: any) {
    this.showPage = page;

    console.log(this.showPage);
    this.closeNav();
  }

  deleteItem(id: any) {
    console.log(id);
    const todelete = confirm('Are you sure you want to delete');
    if (todelete) {
      this.api.deleteInventoryItem(id).subscribe(d => { console.log('Deleted'); this.ngOnInit(); });
    }
  }
}
