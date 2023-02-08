import { Injectable } from '@angular/core';
import { Roles } from '../../../../../data/enums/roles';
import { Log } from '../../../../../data/interfaces/log';
import { Server } from '../../../../../data/interfaces/server';
import { User } from '../../../../../data/interfaces/user';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  route = 'user/'

  constructor(private readonly apiService: ApiService) { }

  getSelf() {
    return this.apiService.get<User>(`${this.route}self`);
  }

  getUserById(id: string) {
    return this.apiService.get<User>(`${this.route}by-id/${id}`);
  }

  getFriendship(friendId: string) {
    return this.apiService.get<string>(`${this.route}friendship/${friendId}`);
  }

  getLogs(friendId: string) {
    return this.apiService.get<Log[]>(`${this.route}logs/${friendId}`);
  }

  sendMessage(friendId: string, message: string) {
    return this.apiService.post<Log>(`${this.route}log/${friendId}`, { 'message': message });
  }

  deleteMessage(friendId: string, logId: string) {
    return this.apiService.delete<any>(`${this.route}log/${friendId}/${logId}`);
  }

  getFriends() {
    return this.apiService.get<User[]>(`${this.route}friends`);
  }

  getServers() {
    return this.apiService.get<Server[]>(`${this.route}servers`);
  }

  joinServer(serverId: string, role: Roles) {
    return this.apiService.post<any>(`${this.route}server/${serverId}/${role}`);
  }

  leaveServer(serverId: string) {
    return this.apiService.delete<any>(`${this.route}server/${serverId}`);
  }

  addFriend(friendId: string) {
    return this.apiService.post<any>(`${this.route}friend/${friendId}`);
  }

  removeFriend(friendId: string) {
    return this.apiService.delete<any>(`${this.route}friend/${friendId}`);
  }

  deleteAccount() {
    return this.apiService.delete<any>(`${this.route}`);
  }
}
