import { Component } from '@angular/core';
import { CustomerSidebar } from '../customer-sidebar/customer-sidebar';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-customer-layout',
  imports: [CustomerSidebar, RouterOutlet],
  templateUrl: './customer-layout.html',
  styleUrl: './customer-layout.css',
})
export class CustomerLayout {}
