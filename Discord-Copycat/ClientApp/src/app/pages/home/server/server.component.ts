import { Clipboard } from '@angular/cdk/clipboard';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Roles } from '../../../../data/enums/roles';
import { Chat } from '../../../../data/interfaces/chat';
import { Server } from '../../../../data/interfaces/server';
import { ChatService } from '../../../core/services/api/chat/chat.service';
import { ServerService } from '../../../core/services/api/server/server.service';
import { CreateChatDialogComponent } from './create-chat-dialog/create-chat-dialog.component';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {
  serverId: string = '';
  server: Server = { id: '', description: '', name: '' };

  chats: Chat[] = [{ id: '1', name: 'Chat1' }];

  Roles = Roles;
  role: Roles = Roles.user;

  constructor(public readonly clipboard: Clipboard, public readonly matDialog: MatDialog, public readonly route: ActivatedRoute, public readonly serverService: ServerService, public readonly chatService: ChatService) {
    this.route.params.subscribe(
      params => {
        this.serverId = params.serverId ?? '';

        this.serverService.getChats(this.serverId).subscribe(
          chats => {
            if (chats.length > 0) {
              this.chats = chats;
            }

            this.serverService.getRole(this.serverId).subscribe(
              role => this.role = role,
              error => {
                console.log(`Error getting role in server ${this.serverId}`);
                console.error(error);
              }
            );
          },
          error => {
            console.log(`Error getting chats of server ${this.serverId}.`);
            console.error(error);
          }
        );

        this.serverService.getServer(this.serverId).subscribe(
          server => this.server = server,
          error => {
            console.log(`Error getting server information for ${this.serverId}.`);
            console.error(error);
          }
        );
      }
    );
  }

  openAddChat() {
    const dialogRef = this.matDialog.open(CreateChatDialogComponent, {
      width: '300px',
      enterAnimationDuration: '0.5s',
    });

    dialogRef.afterClosed().subscribe(
      form => {
        if (form != null) {
          this.chatService.create(this.server.id, form.data).subscribe(
            () => window.location.reload(),
            error => {
              console.log('Error creating new chat.');
              console.error(error);
            }
          );
        }
      },
      error => console.error(error)
    );
  }

  onCopyServerLink() {
    this.serverService.getServerLink(this.server.id).subscribe(
      result => this.clipboard.copy(window.location.origin + '/join-server/' + result.token),
      error => {
        console.log('Error copying server link.');
        console.error(error);
      }
    );
  }

  onLeaveServer() {

  }

  onDeleteServer() {

  }

  ngOnInit(): void {
  }

}
