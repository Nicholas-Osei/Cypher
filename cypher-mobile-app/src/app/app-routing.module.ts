import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then(m => m.HomePageModule)
  },
  {
    path: 'game-screen',
    loadChildren: () => import('./game-screen/game-screen.module').then(m => m.GameScreenPageModule)
  },
  {
    path: 'map-screen',
    loadChildren: () => import('./map-screen/map-screen.module').then(m => m.MapScreenPageModule)
  },
  {
    path: 'inventory',
    loadChildren: () => import('./inventory/inventory.module').then(m => m.InventoryPageModule)
  },
  {
    path: 'cli',
    loadChildren: () => import('./cli/cli.module').then(m => m.CliPageModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'decryption',
    loadChildren: () => import('./decryption/decryption.module').then( m => m.DecryptionPageModule)
  },
  {
    path: 'worm-in-the-system',
    loadChildren: () => import('./worm-in-the-system/worm-in-the-system.module').then( m => m.WormInTheSystemPageModule)
  },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
