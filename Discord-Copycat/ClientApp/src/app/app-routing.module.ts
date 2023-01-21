import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnloggedGuard } from './core/guards/unlogged/unlogged.guard';

const routes: Routes = [
  {
    path: 'auth',
    canActivate: [UnloggedGuard],
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule),
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [UnloggedGuard]
})
export class AppRoutingModule { }
