import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs'
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly route = 'user';

  private isAuthenticatedSubject = new ReplaySubject<boolean>();
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(private readonly apiService: ApiService) { }

  login(user: any) {
    return this.apiService.post<any>(this.route + '/login', user).pipe(
      map((response: any) => {
        if (response) {
          var users = JSON.parse(localStorage.getItem('users') ?? '[]');

          if (users.find((u: string) => u === response.token) == undefined) {
            users.push(response.token);
            localStorage.setItem('users', JSON.stringify(users));
          }
          sessionStorage.setItem('token', response.token);
          window.location.reload();
        }
      })
    );
  }

  logout() {
    var token = sessionStorage.getItem('token');
    var users = JSON.parse(localStorage.getItem('users') ?? '[]');

    users = users.filter((user: string) => user !== token);
    console.log(users);

    localStorage.setItem('users', JSON.stringify(users));
    window.location.reload();
  }

  register(user: any) {
    return this.apiService.post<any>(this.route + '/register', user);
  }

  isLoggedIn() {
    const token = sessionStorage.getItem('token');
    return (token != null);
  }
}
