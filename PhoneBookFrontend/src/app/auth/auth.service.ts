import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from './user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:8080/api/auth';
  currentUserSig = signal<string | undefined | null>(undefined);

  constructor(private http: HttpClient, private router: Router) {}

  login(login: string, password: string) {
    return this.http
      .post<{ token: string }>(`${this.apiUrl}/login`, { login, password })
      .subscribe((response) => {
        localStorage.setItem('token', response.token);
        this.currentUserSig.set(response.token);
        this.router.navigate(['/']);
      });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }

  isAuthenticated(): boolean {
    var isLocalStorageAvailable = typeof localStorage !== 'undefined';
    if (isLocalStorageAvailable) {
      return !!localStorage.getItem('token');
    }
    return false;
  }
}
