import { Injectable } from '@angular/core';
import { Log } from '../../../../../data/interfaces/Log';
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

  sendMessage(chatId: string, message: string) {
    var body = { "message": message };
    return this.apiService.put<Log>(`${this.route}send-message/${chatId}`, JSON.stringify(body));
  }

  getLogs(chatId: string) {
    return this.apiService.get<User[]>(`${this.route}get-logs/${chatId}`);
  }
}
