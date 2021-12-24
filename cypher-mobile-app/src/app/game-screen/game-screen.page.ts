import { ThisReceiver } from '@angular/compiler';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { Inventory, Player, PlayerService } from '../Services/player.service';




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

  constructor(public loginservice: LoginService, public speler: PlayerService, public router: Router) {
    this.imageUrl = './assets/icon/person' + 1 + '.jpeg';
    this.titel = 'Welcome, ' + loginservice.displayName + '!';
  }
  ngOnInit(): void {
    this.speler.getAllPlayers().subscribe(p => {
      this.speler.players = p; console
        .log('Got all players');
      console.log(this.speler.players.data[0].name);
      this.getUserItems();
      // this.getPlayerbyId();
      console.log(this.speler.inventory);
    });

    this.intervalId = setInterval(() => {
      this.time = new Date();
    }, 1000);
  }
  ngOnDestroy() {
    clearInterval(this.intervalId);
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
    this.speler.players.data.forEach(m => {
      this.teller++;
      // console.log(this.loginservice.displayName);
      console.log(this.speler.players.data.length, this.teller);
      if (m.name === this.loginservice.displayName) {
        //
        // this.speler.inventory = m.inventory.items;
        // this.messages = m.messages;
        // this.lobbies = m.playerLobbies;
        // this.id = m.id;
        this.speler.playerId = m.id;
        console.log(m.name);
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
      this.speler.postPlayer(credentials).subscribe(a => { console.log('Player Added'); window.location.reload(); });
    }
    this.getPlayerbyId();
  }

  getPlayerbyId() {
    this.speler.getPlayerById(this.speler.playerId).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.friends);
        this.friends = this.playerbyId.data.friends;
        this.speler.inventory = this.playerbyId.data.inventory;
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
    this.speler.searchForFriends(this.playername).
      subscribe(s => { this.playerSearchResults = s.data; console.log(this.playerSearchResults); });
  }


  deleteFriend(id: number) {
    this.speler.deleteFriend(this.speler.playerId, id).subscribe(d => { console.log('deleted'); this.ngOnInit(); });
  }

  addFriend(id: number) {
    console.log(this.speler.playerId);

    const newFriend = {
      playerId: this.speler.playerId,
      friendId: id
    };
    // eslint-disable-next-line max-len
    this.speler.addToPlayerFriends(this.id, newFriend).subscribe(f => { console.log('Friend Added'); this.generateRandomNumber(); this.ngOnInit(); });
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
      this.speler.deleteInventoryItem(id).subscribe(d => { console.log('Deleted'); this.ngOnInit(); });
    }
  }
}
