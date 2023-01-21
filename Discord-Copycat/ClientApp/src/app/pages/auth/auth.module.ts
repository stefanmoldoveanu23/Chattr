import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthRoutingModule } from './auth-routing.module';

import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthComponent } from './auth.component';

@NgModule({
  declarations: [AuthComponent, RegisterComponent, LoginComponent],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    AuthRoutingModule,
  ]
})
export class AuthModule { }
