import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { CliPageRoutingModule } from './cli-routing.module';

import { CliPage } from './cli.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CliPageRoutingModule
  ],
  declarations: [CliPage]
})
export class CliPageModule {}
