import { Injectable } from '@angular/core';
import { Chat } from '../../../../../data/interfaces/chat';
import { Log } from '../../../../../data/interfaces/log';
import { User } from '../../../../../data/interfaces/user';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  route = 'chat/'

  constructor(private readonly apiService: ApiService) { }

  create(serverId: string, chat: any) {
    return this.apiService.post<Chat>(`${this.route}${serverId}`, chat);
  }

  getUsers(chatId: string) {
    return this.apiService.get<User[]>(`${this.route}${chatId}/users`);
  }

  sendMessage(chatId: string, message: string) {
    return this.apiService.post<Log>(`${this.route}${chatId}/log`, { 'message': message });
  }

  getLogs(chatId: string) {
    return this.apiService.get<User[]>(`${this.route}${chatId}/logs`);
  }
}
