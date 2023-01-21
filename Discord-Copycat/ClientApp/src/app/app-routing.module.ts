import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoggedGuard } from './core/guards/logged/logged.guard';
import { UnloggedGuard } from './core/guards/unlogged/unlogged.guard';
import { AuthComponent } from './pages/auth/auth.component';
import { HomeComponent } from './pages/home/home.component';

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
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [UnloggedGuard]
})
export class AppRoutingModule { }
