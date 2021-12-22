import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { Player, PlayerService } from '../Services/player.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.page.html',
  styleUrls: ['./inventory.page.scss'],
})
export class InventoryPage implements OnInit {

  // players: Player;
  // pf: any;
  inventory: any;
  inInventory = true;
  id: any;
  playerbyId: any;
  teller = 0;
  constructor(public speler: PlayerService, public loginservice: LoginService, public router: Router) { }

  ngOnInit() {
    this.speler.getAllPlayers().subscribe(p => {
      this.speler.players = p; console
        .log('Got all players');
      this.speler.players.data.forEach(m => {
        if (m.name === this.loginservice.displayName) {
          console.log(m.name);
          // this.id = m.id;
          this.getPlayerbyId(m.id);
        }
        // else if (this.speler.players.data.length === this.teller) {
        //   console.log('ik ben hier');
        // };
      });
    });

  }
  goToMapscreen() {
    this.router.navigate(['map-screen']);
    this.inInventory = false;
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string) {
    console.log('Called open ' + page);
    this.router.navigate([page]).then(() => window.location.reload());
  }

  getPlayerbyId(pId) {
    this.speler.getPlayerById(pId).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.inventory);
        this.inventory = this.playerbyId.data.inventory.items;
        // this.speler.inventory = this.playerbyId.data.inventory;
        // this.speler.messages = m.messages;
        // this.lobbies = m.playerLobbies;
      });
  }
  addItemToInventory(form) {
    let newItems: any = [];
    const item = [];
    console.log('clicked');
    // this.playerbyId.data.inventory.forEach(m => {
    // if (m.name === this.loginservice.displayName) {
    // eslint-disable-next-line @typescript-eslint/prefer-for-of
    for (let index = 0; index < this.playerbyId.data.inventory.items.length; index++) {
      item.push(
        {
          name: this.playerbyId.data.inventory.items[index].name,
          itemType: this.playerbyId.data.inventory.items[index].itemType
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
      id: this.playerbyId.data.inventory.id,
      items: item
    };
    return this.speler.updateInventoryItems(this.playerbyId.data.inventory.id, newItems).
      subscribe(l => { console.log('added'); form.value.naam = ''; form.value.itemType = ''; this.ngOnInit(); });
    //}

    // });

  }
  deleteItem(id: any) {
    console.log(id);
    const todelete = confirm('Are you sure you want to delete');
    if (todelete) {
      this.speler.deleteInventoryItem(id).subscribe(d => { console.log('Deleted'); this.ngOnInit(); });
    }
  }

}
