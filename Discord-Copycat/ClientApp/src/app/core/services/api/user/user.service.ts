import { Injectable } from '@angular/core';
import { roles } from '../../../../../data/enums/roles';
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

  getFriends() {
    return this.apiService.get<User[]>(`${this.route}get-friends`);
  }

  getServers() {
    return this.apiService.get<Server[]>(`${this.route}get-servers`);
  }

  joinServer(serverId: string, role: roles) {
    return this.apiService.post<any>(`${this.route}join-server/${serverId}/${role}`);
  }

  addFriend(friendId: string) {
    return this.apiService.post<any>(`${this.route}add-friend/${friendId}`);
  }

  deleteAll() {
    return this.apiService.delete<any>(`${this.route}`);
  }
}
