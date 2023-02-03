import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Roles } from '../../../../data/enums/roles';
import { Chat } from '../../../../data/interfaces/chat';
import { Server } from '../../../../data/interfaces/server';
import { ServerService } from '../../../core/services/api/server/server.service';

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

  constructor(public readonly route: ActivatedRoute, public readonly serverService: ServerService) {
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

  ngOnInit(): void {
  }

}
