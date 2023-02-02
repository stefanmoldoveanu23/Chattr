import { Injectable } from '@angular/core';
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

  getChats(serverId: string) {
    return this.apiService.get<>
  }
}
