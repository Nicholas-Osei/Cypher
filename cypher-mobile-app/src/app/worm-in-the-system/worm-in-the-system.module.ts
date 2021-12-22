import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { WormInTheSystemPageRoutingModule } from './worm-in-the-system-routing.module';

import { WormInTheSystemPage } from './worm-in-the-system.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    WormInTheSystemPageRoutingModule
  ],
  declarations: [WormInTheSystemPage]
})
export class WormInTheSystemPageModule {}
