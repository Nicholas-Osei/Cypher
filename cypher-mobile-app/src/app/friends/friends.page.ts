import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { ApiService } from '../Services/api.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.page.html',
  styleUrls: ['./friends.page.scss'],
})
export class FriendsPage implements OnInit {

  randomNumber = 0;
  imageUrl: any;
  playername = '';
  playerSearchResults: any;
  friends: any;
  playerbyId: any;
  id: number;

  constructor(public api: ApiService, public loginservice: LoginService, public router: Router) { }


  ngOnInit() {
    this.api.getAllPlayers().subscribe(p => {
      this.api.players = p; console
        .log('Got all players');
      this.api.players.data.forEach(m => {
        if (m.name === this.loginservice.displayName) {
          console.log(m.name);
          this.id = m.id;
          this.getPlayerbyId(m.id);
        }
      });
    });
  }

  deleteFriend(id: number) {
    this.api.deleteFriend(this.id, id).subscribe(d => { console.log('deleted'); this.ngOnInit(); });
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string) {
    console.log('Called open ' + page);
    this.router.navigate([page]).then(() => window.location.reload());
  }
  getPlayerbyId(pId) {
    this.api.getPlayerById(pId).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.inventory);
        this.friends = this.playerbyId.data.friends;
      });
  }
  addFriend(id: number) {
    console.log(this.id);

    const newFriend = {
      playerId: this.id,
      friendId: id
    };
    // eslint-disable-next-line max-len
    this.api.addToPlayerFriends(this.id, newFriend).subscribe(f => { console.log('Friend Added'); this.generateRandomNumber(); this.ngOnInit(); });
  }

  generateRandomNumber() {
    // eslint-disable-next-line prefer-const
    this.randomNumber = Math.floor((Math.random() * 3) + 1);
    this.imageUrl = './assets/icon/person' + this.randomNumber + '.jpeg';
    console.log(this.randomNumber);
    return this.randomNumber;
  }

  search(name?: any) {
    console.log(this.playername);
    // console.log(this.)
    this.api.searchForFriends(this.playername).
      subscribe(s => { this.playerSearchResults = s.data; console.log(this.playerSearchResults); });
  }
}
