import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TouristPage } from './tourist.page';

const routes: Routes = [
  {
    path: '',
    component: TouristPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TouristPageRoutingModule {}
