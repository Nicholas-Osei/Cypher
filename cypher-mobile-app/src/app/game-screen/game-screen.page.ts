import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { Player, PlayerService } from '../Services/player.service';

@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.page.html',
  styleUrls: ['./game-screen.page.scss'],
})
export class GameScreenPage {

  players: Player;
  inventory: any;

  constructor(public loginservice: LoginService, public speler: PlayerService) {
    this.speler.getAllPlayers().subscribe(p => {
      this.players = p; console
        .log('Got all players');
      console.log(this.players.data[0].name);
    });

  }

  getUserItems() {
    this.players.data.forEach(m => {
      if (m.name === this.loginservice.displayName) {
        this.inventory = m.inventory.items;
      }
    });
  }
}
