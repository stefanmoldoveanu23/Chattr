import { Injectable } from '@angular/core';
import { Roles } from '../../../../../data/enums/roles';
import { Chat } from '../../../../../data/interfaces/chat';
import { Server } from '../../../../../data/interfaces/server';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  route = 'server/'

  constructor(private readonly apiService: ApiService) { }

  create(server: any) {
    return this.apiService.post<Server>(`${this.route}create`, server);
  }

  getServer(serverId: string) {
    return this.apiService.get<Server>(`${this.route}get-server/${serverId}`);
  }

  getRole(serverId: string) {
    return this.apiService.get<Roles>(`${this.route}get-role/${serverId}`);
  }

  getChats(serverId: string) {
    return this.apiService.get<Chat>(`${this.route}get-chats-for-user/${serverId}`);
  }
}
