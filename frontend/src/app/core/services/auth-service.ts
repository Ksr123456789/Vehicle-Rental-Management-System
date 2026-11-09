import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../Enviroment/environment';
import { jwtDecode } from 'jwt-decode';

export interface authResponse {
  accessToken: string;
  refreshToken: string;
}

export interface loginDto {
  email: string | undefined;
  password: string | undefined;
}

export interface registerDto {
  FullName: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
  PhoneNumber: string;
  DrivingLicenseNumber: string;
  LicenseExpiryDate: string;
  Address: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http = inject(HttpClient);

  currentUserSignal = signal<authResponse | null>(null);

  public currentUser = this.currentUserSignal.asReadonly();

  login(data: loginDto) {
    return this.http.post<authResponse>(`${environment.apiUrl}/Auth/Login`, data);
  }

  logout() {
    return this.http.post(`${environment.apiUrl}/Auth/Logout`, {});
  }

  register(data: registerDto) {
    return this.http.post<authResponse>(`${environment.apiUrl}/Auth/Register`, data);
  }

  refreshToken() {
    const stored = localStorage.getItem('auth');

    if (!stored) {
      throw new Error("auth doesn't exist");
    }

    const tokens = JSON.parse(stored);

    return this.http.post<authResponse>(`${environment.apiUrl}/Auth/Refresh`, {
      accessToken: tokens.accessToken,
      refreshToken: tokens.refreshToken,
    });
  }
  restoreUser() {
    const stored = localStorage.getItem('auth');

    if (stored) {
      this.currentUserSignal.set(JSON.parse(stored));
    }
  }

  setUser(data: authResponse) {
    localStorage.setItem('auth', JSON.stringify(data));

    this.currentUserSignal.set(data);
  }

  clearUser() {
    localStorage.removeItem('auth');
    this.currentUserSignal.set(null);
  }
  isAuthenticated() {
    return this.currentUser() !== null;
  }

  getDecodedToken(): any | null {
    const user = this.currentUser();

    if (!user) return null;

    try {
      return jwtDecode(user.accessToken);
    } catch {
      return null;
    }
  }

  getRole(): string | null {
    const decoded = this.getDecodedToken();

    if (!decoded) return null;

    return decoded.role ?? null;
  }
  getEmail(): string | null {
    const decoded = this.getDecodedToken();

    if (!decoded) return null;

    return decoded.email ?? null;
  }

  getDrivingLicense(): string | null {
    const decoded = this.getDecodedToken();

    if (!decoded) return null;
    return decoded['driving_license'] ?? null;
  }
}
