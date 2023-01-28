import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoggedGuard implements CanActivate {

  constructor(private readonly authService: AuthService, private readonly router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (sessionStorage.getItem('token') != null) {
      const token = sessionStorage.getItem('token');
      const users = JSON.parse(localStorage.getItem('users') ?? '[]');

      if (!users.find((user: string) => user === token)) {
        sessionStorage.removeItem('token');
      }
    } else if (localStorage.getItem('users') != '[]') {
      const obj = JSON.parse(localStorage.getItem('users') ?? '[]');
      console.log(obj);

      if (obj.length != 0) {
        sessionStorage.setItem('token', obj.at(-1));
      }
    }

    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigate(['/auth/login']);
      return false;
    }

  }
  
}
