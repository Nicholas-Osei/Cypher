import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MapScreenPageRoutingModule } from './map-screen-routing.module';

import { MapScreenPage } from './map-screen.page';
import { TouristPage } from '../tourist/tourist.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MapScreenPageRoutingModule
  ],
  declarations: [MapScreenPage, TouristPage]
})
export class MapScreenPageModule { }
