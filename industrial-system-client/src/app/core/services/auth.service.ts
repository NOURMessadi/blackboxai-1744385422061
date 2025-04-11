import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'auth_token';

  constructor(
    private api: ApiService,
    private router: Router
  ) {}

  login(credentials: LoginRequest) {
    return this.api.post<LoginResponse>('auth/login', credentials).pipe(
      tap(response => {
        this.setToken(response.token);
      })
    );
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
}
