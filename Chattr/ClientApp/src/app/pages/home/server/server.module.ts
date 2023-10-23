import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerRouterModule } from './server-router.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio'
import { MatMenuModule } from '@angular/material/menu';

import { ServerComponent } from './server.component';
import { CreateChatDialogComponent } from './create-chat-dialog/create-chat-dialog.component';



@NgModule({
  declarations: [ServerComponent, CreateChatDialogComponent],
  imports: [
    CommonModule,
    ServerRouterModule,
    FormsModule,
    ReactiveFormsModule,
    ClipboardModule,
    MatSidenavModule,
    MatIconModule,
    MatDialogModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatMenuModule,
  ]
})
export class ServerModule { }
