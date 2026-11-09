import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-customer-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './customer-sidebar.html',
  styleUrl: './customer-sidebar.css',
})
export class CustomerSidebar {


  authService = inject(AuthService);
  router = inject(Router);

  logoutCustomer(){
    this.authService.logout().subscribe({
      next : () =>{
        console.log("logged out");
        this.authService.clearUser();
        this.router.navigate(["/login"])
      }
    })
  }

}
