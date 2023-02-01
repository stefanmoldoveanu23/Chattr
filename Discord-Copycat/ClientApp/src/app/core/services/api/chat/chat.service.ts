import { Injectable } from '@angular/core';
import { User } from '../../../../../data/interfaces/user';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  route = 'chat/'

  constructor(private readonly apiService: ApiService) { }

  getUsers(chatId: string) {
    return this.apiService.get<User[]>(`${this.route}get-users/${chatId}`);
  }

  getLogs(chatId: string) {
    return this.apiService.get<User[]>(`${this.route}get-logs/${chatId}`);
  }
}
