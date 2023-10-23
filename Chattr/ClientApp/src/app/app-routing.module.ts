import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoggedGuard } from './core/guards/logged/logged.guard';
import { UnloggedGuard } from './core/guards/unlogged/unlogged.guard';
import { AuthComponent } from './pages/auth/auth.component';
import { HomeComponent } from './pages/home/home.component';
import { JoinServerComponent } from './pages/join-server/join-server.component';

const routes: Routes = [
  {
    path: 'home',
    canActivate: [LoggedGuard],
    component: HomeComponent,
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'auth',
    canActivate: [UnloggedGuard],
    component: AuthComponent,
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule),
  },
  {
    path: 'join-server/:token',
    canActivate: [LoggedGuard],
    component: JoinServerComponent,
  },
  {
    path: '**',
    redirectTo: 'home'
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [UnloggedGuard]
})
export class AppRoutingModule { }
