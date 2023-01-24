import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  route = 'server/'

  constructor(private readonly apiService: ApiService) { }

  create(server: any) {
    return this.apiService.post(`${this.route}create`, server);
  }
}
