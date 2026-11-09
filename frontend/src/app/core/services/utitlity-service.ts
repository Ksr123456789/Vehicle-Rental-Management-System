import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtitlityService {
  getBookingStatus(value: number) {
    switch (value) {
      case 0:
        return 'Pending';
      case 1:
        return 'Confirmed';
      case 2:
        return 'Active';
      case 3:
        return 'Completed';
      case 4:
        return 'Cancelled';
      default:
        return 'unknown';
    }
  }

  getCompanyStatus(value: number) {
    switch (value) {
      case 0:
        return 'Active';
      case 1:
        return 'InActive';
      default:
        return 'unknown';
    }
  }

  formatLocalDate(utcDateTime: string): string {
    const date = new Date(utcDateTime + 'Z');

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  formatLocalTime(utcDateTime: string): string {
    const date = new Date(utcDateTime + 'Z');

    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    return `${hours}:${minutes}`;
  }

  getCompanyType(value: number) {
    switch (value) {
      case 0:
        return 'Budget';
      case 1:
        return 'Economy';
      case 2:
        return 'Premium';
      case 3:
        return 'Luxury';
      default:
        return 'unknown';
    }
  }

  getVehicleAvailabilityStatus(value: number) {
    switch (value) {
      case 0:
        return 'Available';
      case 1:
        return 'Unavailable';
      case 2:
        return 'InMaintenance';
      default:
        return 'unknown';
    }
  }
  convertToCapital(value: string) {
    return value.toUpperCase();
  }
}
