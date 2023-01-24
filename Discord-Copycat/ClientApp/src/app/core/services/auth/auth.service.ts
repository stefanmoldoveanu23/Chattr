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
          localStorage.setItem('token', response.token);
        } else {
          console.log("HELLO");
        }
      })
    );
  }

  register(user: any) {
    return this.apiService.post<any>(this.route + '/register', user);
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return (token != null);
  }
}
