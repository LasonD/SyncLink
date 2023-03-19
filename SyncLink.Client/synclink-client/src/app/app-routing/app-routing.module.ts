import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreloadAllModules, PreloadingStrategy, RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
  { path: '', redirectTo: '/groups', pathMatch: 'full' },
  { path: 'auth', loadChildren: () => import('../auth/auth.module').then(m => m.AuthModule) },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes, { preloadingStrategy: PreloadAllModules, initialNavigation: 'enabledBlocking' }),
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
