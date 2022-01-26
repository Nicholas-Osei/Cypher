import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Services/login.service';
import { Player, ApiService } from '../Services/api.service';
import { InventoryService } from '../Services/inventory.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.page.html',
  styleUrls: ['./inventory.page.scss'],
})
export class InventoryPage implements OnInit {

  inInventory = true;

  constructor(public api: ApiService, public loginservice: LoginService, public router: Router, public inventory: InventoryService) { }

  ngOnInit() {

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
    this.inventory.getPlayerbyId(pId);
  }
  addItemToInventory(form) {
    this.inventory.addItemToInventory(form);
  }
  deleteItem(id: any) {
    this.inventory.deleteItem(id);
  }
}
