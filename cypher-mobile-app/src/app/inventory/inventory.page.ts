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
  constructor(public speler: PlayerService, public loginservice: LoginService, public router: Router) { }

  ngOnInit() {
    this.speler.getAllPlayers().subscribe(p => {
      this.speler.players = p; console
        .log('Got all players');
      this.speler.players.data.forEach(m => {
        if (m.name === this.loginservice.displayName) {
          this.inventory = m.inventory.items;
        };
      });
    });

  }
  goToMapscreen() {
    this.router.navigate(['map-screen']);
    this.inInventory = false;
  }

  addItemToInventory(form) {
    let newItems: any = [];
    const item = [];
    console.log('clicked');
    this.speler.players.data.forEach(m => {
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
  deleteItem(id: any) {
    console.log(id);
    const todelete = confirm('Are you sure you want to delete');
    if (todelete) {
      this.speler.deleteInventoryItem(id).subscribe(d => { console.log('Deleted'); this.ngOnInit(); });
    }
  }

}
