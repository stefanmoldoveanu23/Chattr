import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HomeComponent } from './home.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { CreateServerDialogComponent } from './create-server-dialog/create-server-dialog.component';
import { MatMenuModule } from '@angular/material/menu';
import { ChatComponent } from './chat/chat.component';



@NgModule({
  declarations: [HomeComponent, CreateServerDialogComponent, ChatComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatListModule,
    MatSidenavModule,
    MatIconModule,
    MatDialogModule,
    MatCardModule,
    MatMenuModule,
    ClipboardModule,
    HomeRoutingModule,
  ]
})
export class HomeModule { }
