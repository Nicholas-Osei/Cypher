import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CliPage } from './cli.page';

const routes: Routes = [
  {
    path: '',
    component: CliPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CliPageRoutingModule {}
