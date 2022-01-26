import { Injectable } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { ApiService } from '../Services/api.service';

@Injectable({
  providedIn: 'root'
})

export class InventoryService {

  inventory: any;
  inInventory = true;
  id: any;
  playerbyId: any;
  teller = 0;
  itemLength: any;
  invInLocalStorage: any;

  constructor(public api: ApiService, public loginservice: LoginService) {
    this.refreshConnection();
  }
  
  refreshConnection(){
    this.api.getAllPlayers().subscribe(p => {
      this.api.players = p;
      this.api.players.data.forEach(m => {
        if (m.name === this.loginservice.displayName) {
          this.getPlayerbyId(m.id);
          localStorage.setItem('playerId', JSON.stringify(m.id));
        }
      });
    });
    
  }

  getPlayerbyId(pId) {
    this.api.getPlayerById(pId).subscribe(
      u => {
        console.log('Got friends'); this.playerbyId = u;
        console.log(this.playerbyId.data.inventory);
        this.inventory = this.playerbyId.data.inventory.items;
        this.itemLength = localStorage.setItem('inventoryLength', this.playerbyId.data.inventory.items.length);
        this.invInLocalStorage = localStorage.setItem('inventoryItems', this.inventory);
        console.log(this.inventory);
      });
  }
  
  GetInventory(){
    this.refreshConnection();
    return this.inventory;
  }

  addItemToInventory(form) {
    let newItems: any = [];
    const item = [];

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

    return this.api.updateInventoryItems(this.playerbyId.data.inventory.id, newItems).
      subscribe(l => { console.log('added'); form.value.naam = ''; form.value.itemType = ''; this.refreshConnection(); });
  }

  deleteItem(id: any) {
    const todelete = confirm('Are you sure you want to delete');
    if (todelete) {
      this.api.deleteInventoryItem(id).subscribe(d => { console.log('Deleted'); this.refreshConnection(); });
    }
  }
}
