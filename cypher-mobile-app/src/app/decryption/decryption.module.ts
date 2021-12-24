import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DecryptionPageRoutingModule } from './decryption-routing.module';

import { DecryptionPage } from './decryption.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DecryptionPageRoutingModule
  ],
  declarations: [DecryptionPage]
})
export class DecryptionPageModule {}
