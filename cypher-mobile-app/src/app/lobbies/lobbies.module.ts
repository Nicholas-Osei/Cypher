import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { LobbiesPageRoutingModule } from './lobbies-routing.module';

import { LobbiesPage } from './lobbies.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    LobbiesPageRoutingModule
  ],
  declarations: [LobbiesPage]
})
export class LobbiesPageModule {}
