import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DecryptionPage } from './decryption.page';

const routes: Routes = [
  {
    path: '',
    component: DecryptionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DecryptionPageRoutingModule {}
