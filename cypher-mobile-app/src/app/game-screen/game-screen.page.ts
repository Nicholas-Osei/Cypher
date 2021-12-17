import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { Inventory, Player, PlayerService } from '../Services/player.service';



@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.page.html',
  styleUrls: ['./game-screen.page.scss'],
})
export class GameScreenPage implements OnInit {

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

  lol = Array(5);
  titel = '';
  teller = 0;
  notViaGoogle = false;

  constructor(public loginservice: LoginService, public speler: PlayerService, public router: Router) {
    // this.speler.getAllPlayers().subscribe(p => {
    //   this.players = p; console
    //     .log('Got all players');
    //   console.log(this.players.data[0].name);
    // });
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

  }

  goToMap() {
    this.router.navigate(['map-screen'],
    ).then(() => { window.location.reload(); });
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
        this.id = m.id;
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
    this.speler.getPlayerById(this.id).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.friends);
        this.friends = this.playerbyId.data.friends;
        this.speler.inventory = this.playerbyId.data.inventory;
        // this.speler.messages = m.messages;
        // this.lobbies = m.playerLobbies;
      });
  }

  search() {
    console.log(this.playername);
    // console.log(this.)
    this.speler.searchForFriends(this.playername).
      subscribe(s => { this.playerSearchResults = s.data; console.log(this.playerSearchResults); });
  }

  deleteFriend(id: number) {
    this.speler.deleteFriend(this.id, id).subscribe(d => { console.log('deleted'); this.ngOnInit(); });
  }

  addFriend(id: number) {
    console.log(this.id);
    const newFriend = {
      playerId: this.id,
      friendId: id
    };
    this.speler.addToPlayerFriends(this.id, newFriend).subscribe(f => { console.log('Friend Added'); this.ngOnInit(); });
  }
  // addItemToInventory(form) {
  //   let newItems: any = [];
  //   const item = [];
  //   console.log('clicked');
  //   this.players.data.forEach(m => {
  //     if (m.name === this.loginservice.displayName) {
  //       // eslint-disable-next-line @typescript-eslint/prefer-for-of
  //       for (let index = 0; index < m.inventory.items.length; index++) {
  //         item.push(
  //           {
  //             name: m.inventory.items[index].name,
  //             itemType: m.inventory.items[index].itemType
  //           },
  //         );
  //       }
  //       item.push(
  //         {
  //           name: form.value.naam,
  //           itemType: form.value.itemType
  //         }
  //       );
  //       newItems = {
  //         id: m.inventory.id,
  //         items: item
  //       };
  //       return this.speler.updateInventoryItems(m.inventory.id, newItems).
  //         subscribe(l => { console.log('added'); form.value.naam = ''; form.value.itemType = ''; this.ngOnInit(); });
  //     }

  //   });

  // }

  openNav() {
    document.getElementById('mySidenav').style.width = '250px';
  }

  closeNav() {
    document.getElementById('mySidenav').style.width = '0';
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
