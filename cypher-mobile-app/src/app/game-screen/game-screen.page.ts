import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { Inventory, Player, PlayerService } from '../Services/player.service';


@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.page.html',
  styleUrls: ['./game-screen.page.scss'],
})
export class GameScreenPage implements OnInit {

  players: Player;
  inventory: any;
  showPage: any;
  lobbies: any;
  friends: any;
  messages: any;

  constructor(public loginservice: LoginService, public speler: PlayerService) {
    // this.speler.getAllPlayers().subscribe(p => {
    //   this.players = p; console
    //     .log('Got all players');
    //   console.log(this.players.data[0].name);
    // });

  }
  ngOnInit(): void {
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
        this.messages = m.messages;
        this.lobbies = m.playerLobbies;

      }
    });
  }


  addItemToInventory(form) {
    let newItems: any = [];
    const item = [];
    console.log('clicked');
    this.players.data.forEach(m => {
      if (m.name === this.loginservice.displayName) {
        // eslint-disable-next-line @typescript-eslint/prefer-for-of
        for (let index = 0; index < m.inventory.items.length; index++) {
          item.push(
            {
              name: m.inventory.items[index].name,
              itemType: m.inventory.items[index].itemType
            },
          );
        }
        item.push(
          {
            name: form.value.naam,
            itemType: form.value.itemType
          }
        );
        newItems = {
          id: m.inventory.id,
          items: item
        };
        return this.speler.updateInventoryItems(m.inventory.id, newItems).
          subscribe(l => { console.log('added'); form.value.naam = ''; form.value.itemType = ''; this.ngOnInit(); });
      }

    });

  }

  openNav() {
    document.getElementById('mySidenav').style.width = '250px';
  }

  closeNav() {
    document.getElementById('mySidenav').style.width = '0';
  }
  renderPage(page: any) {
    this.showPage = page;
    this.closeNav();
  }
}
