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

    if (sessionStorage.getItem('token') == null && localStorage.getItem('token') != null) {
      sessionStorage.setItem('token', localStorage.getItem('token') ?? '');
    }

    if (localStorage.getItem('token') == null && sessionStorage.getItem('token') != null) {
      localStorage.setItem('token', sessionStorage.getItem('token') ?? '');
    }

    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigate(['/auth/login']);
      return false;
    }

  }
  
}
