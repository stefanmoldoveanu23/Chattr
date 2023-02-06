import { HttpHeaders } from '@angular/common/http';
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
    return this.apiService.post<Server>(`${this.route}`, server);
  }

  getServer(serverId: string) {
    return this.apiService.get<Server>(`${this.route}by-id/${serverId}`);
  }

  getServerFromToken(token: string) {
    let headers = new HttpHeaders()
      .append('Server', token);
    return this.apiService.get<Server>(`${this.route}by-token`, {}, headers);
  }

  getServerLink(serverId: string) {
    return this.apiService.get<any>(`${this.route}${serverId}/token`);
  }

  getRole(serverId: string) {
    return this.apiService.get<Roles>(`${this.route}${serverId}/role`);
  }

  getChats(serverId: string) {
    return this.apiService.get<Chat>(`${this.route}${serverId}/chats-for-user`);
  }

  delete(serverId: string) {
    return this.apiService.delete<any>(`${this.route}${serverId}`);
  }

  deleteAdmin(serverId: string) {
    return this.apiService.delete<any>(`${this.route}${serverId}/admin`);
  }
}
