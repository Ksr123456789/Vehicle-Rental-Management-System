import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';

import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, catchError, finalize, shareReplay, switchMap, throwError } from 'rxjs';

import { AuthService, authResponse } from '../services/auth-service';

let refreshTokenRequest$: Observable<authResponse> | null = null;

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isRefreshRequest = req.url.includes('/Auth/Refresh');

  if (isRefreshRequest) {
    return next(req);
  }

  const stored = localStorage.getItem('auth');
  const auth = stored ? JSON.parse(stored) : null;

  let authReq = req;

  if (auth?.accessToken) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${auth.accessToken}`,
      },
    });
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status !== 401 || !auth?.refreshToken) {
        return throwError(() => error);
      }

      if (!refreshTokenRequest$) {
        refreshTokenRequest$ = authService.refreshToken().pipe(
          shareReplay(1),

          finalize(() => {
            refreshTokenRequest$ = null;
          }),
        );
      }

      return refreshTokenRequest$.pipe(
        switchMap((newTokens: authResponse) => {
          authService.setUser(newTokens);

          const retryReq = req.clone({
            setHeaders: {
              Authorization: `Bearer ${newTokens.accessToken}`,
            },
          });

          return next(retryReq);
        }),

        catchError((refreshError) => {
          authService.clearUser();
          router.navigate(['/login']);

          return throwError(() => refreshError);
        }),
      );
    }),
  );
};
