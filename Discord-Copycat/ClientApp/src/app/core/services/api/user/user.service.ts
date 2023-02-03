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
    return this.apiService.get<User>(`${this.route}get-self`);
  }

  getUserById(id: string) {
    return this.apiService.get<User>(`${this.route}get-by-id/${id}`);
  }

  getFriendship(friendId: string) {
    return this.apiService.get<string>(`${this.route}get-friendship/${friendId}`);
  }

  getLogs(friendId: string) {
    return this.apiService.get<Log[]>(`${this.route}get-logs/${friendId}`);
  }

  sendMessage(friendId: string, message: string) {
    return this.apiService.put<Log>(`${this.route}send-message/${friendId}`, { 'message': message });
  }

  getFriends() {
    return this.apiService.get<User[]>(`${this.route}get-friends`);
  }

  getServers() {
    return this.apiService.get<Server[]>(`${this.route}get-servers`);
  }

  joinServer(serverId: string, role: Roles) {
    return this.apiService.post<any>(`${this.route}join-server/${serverId}/${role}`);
  }

  addFriend(friendId: string) {
    return this.apiService.post<any>(`${this.route}add-friend/${friendId}`);
  }

  removeFriend(friendId: string) {
    return this.apiService.post<any>(`${this.route}remove-friend/${friendId}`);
  }

  deleteAll() {
    return this.apiService.delete<any>(`${this.route}`);
  }
}
