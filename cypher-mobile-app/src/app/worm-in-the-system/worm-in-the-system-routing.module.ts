import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { WormInTheSystemPage } from './worm-in-the-system.page';

const routes: Routes = [
  {
    path: '',
    component: WormInTheSystemPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WormInTheSystemPageRoutingModule {}
