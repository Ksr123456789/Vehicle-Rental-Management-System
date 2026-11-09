import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth-service';

@Component({
  selector: 'app-admin-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './admin-sidebar.html',
  styleUrl: './admin-sidebar.css',
})
export class AdminSidebar {
  authService = inject(AuthService);
  router = inject(Router);

  logoutAdmin() {
    this.authService.logout().subscribe({
      next: () => {
        console.log('logged out');
        this.authService.clearUser();
        this.router.navigate(['/login']);
      },
    });
  }
}
