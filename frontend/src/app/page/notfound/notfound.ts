import { Component, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notfound',
  imports: [],
  templateUrl: './notfound.html',
  styleUrl: './notfound.css',
})
export class Notfound {
  authService = inject(AuthService);
  router = inject(Router);

  returnHome() {
    if (this.authService.getRole() == 'Admin') {
      this.router.navigate(['/admin']);
    } else if (this.authService.getRole() == 'Customer') {
      this.router.navigate(['/customer']);
    } else {
      this.router.navigate(['/login']);
    }
  }
}
