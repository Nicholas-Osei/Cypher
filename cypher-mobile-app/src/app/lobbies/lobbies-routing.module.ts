import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LobbiesPage } from './lobbies.page';

const routes: Routes = [
  {
    path: '',
    component: LobbiesPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LobbiesPageRoutingModule {}
