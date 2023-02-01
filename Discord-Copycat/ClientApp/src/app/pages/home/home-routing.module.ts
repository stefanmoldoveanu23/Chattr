import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { ServerComponent } from './server/server.component';

const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    loadChildren: () => import('./profile/profile.module').then(p => p.ProfileModule)
  },
  {
    path: ':serverId',
    component: ServerComponent
  },
  {
    path: '**',
    redirectTo: 'profile'
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
