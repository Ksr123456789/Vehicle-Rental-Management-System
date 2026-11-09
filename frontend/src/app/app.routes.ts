import { Routes } from '@angular/router';
import { Login } from './features/Auth/login/login';
import { Register } from './features/Auth/register/register';
import { adminGuard } from './core/guard/admin-guard';
import { AdminRentalCompany } from './features/Admin/admin-rental-company/admin-rental-company';
import { AdminLayout } from './features/Admin/admin-layout/admin-layout';
import { customerGuard } from './core/guard/customer-guard';
import { CustomerLayout } from './features/Customer/customer-layout/customer-layout';
import { CustomerAvailableVehicle } from './features/Customer/customer-available-vehicle/customer-available-vehicle';
import { Notfound } from './page/notfound/notfound';
import { AdminVehicle } from './features/Admin/admin-vehicle/admin-vehicle';
import { AdminBooking } from './features/Admin/admin-booking/admin-booking';
import { CustomerRentals } from './features/Customer/customer-rentals/customer-rentals';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'register',
    component: Register,
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/login',
  },
  {
    path: 'admin',
    canActivate: [adminGuard],
    component: AdminLayout,
    children: [
      {
        path: 'rentalCompany',
        component: AdminRentalCompany,
      },
      {
        path: 'vehicle',
        component: AdminVehicle,
      },
      {
        path: 'bookings',
        component: AdminBooking,
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'rentalCompany',
      },
    ],
  },
  {
    path: 'customer',
    canActivate: [customerGuard],
    component: CustomerLayout,
    children: [
      {
        path: 'available-vehicle',
        component: CustomerAvailableVehicle,
      },
      {
        path: 'my-rentals',
        component: CustomerRentals
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'available-vehicle',
      },
    ],
  },
  {
    path: '**',
    component: Notfound,
  },
];
