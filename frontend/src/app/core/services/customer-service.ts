import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  AvailableVehicle,
  bookVehicleDto,
  cancelRentalDto,
  customerDetailDto,
  ExtraService,
  getAvailableVehicleDto,
  getMyRentalDto,
  MyRentals,
  PagedResult,
  RentalCompany,
  VehicleCategory,
} from '../../Enviroment/model';
import { environment } from '../../Enviroment/environment';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  http = inject(HttpClient);

  getPagedAvailableVehicle(data: getAvailableVehicleDto) {
    let params = new HttpParams();

    params = params.set('PageNumber', data.PageNumber.toString());
    params = params.set('PageSize', data.PageSize.toString());

    if (data.SearchBy) params = params.set('SearchBy', data.SearchBy);
    if (data.SortBy) params = params.set('sortBy', data.SortBy);
    if (data.SortDirection) params = params.set('SortDirection', data.SortDirection);
    if (data.VehicleCategoryId) params = params.set('VehicleCategoryId', data.VehicleCategoryId);
    if (data.SeatingCapacity) params = params.set('SeatingCapacity', data.SeatingCapacity);
    if (data.RentalCompanyId) params = params.set('RentalCompanyId', data.RentalCompanyId);
    if (data.FromDailyRentalRate)
      params = params.set('FromDailyRentalRate', data.FromDailyRentalRate.toString());
    if (data.ToDailyRentalRate)
      params = params.set('ToDailyRentalRate', data.ToDailyRentalRate.toString());

    return this.http.get<PagedResult<AvailableVehicle>>(
      `${environment.apiUrl}/Customer/GetAvailableVehicle`,
      {
        params,
      },
    );
  }

  getVehicleCategory() {
    return this.http.get<VehicleCategory[]>(`${environment.apiUrl}/Customer/GetVehicleCategory`);
  }

  getRentalCompany() {
    return this.http.get<RentalCompany[]>(`${environment.apiUrl}/Customer/GetRentalCompany`);
  }

  getMyPagedRentals(data: getMyRentalDto) {
    let params = new HttpParams();

    params = params.set('PageNumber', data.PageNumber.toString());
    params = params.set('PageSize', data.PageSize.toString());

    if (data.SearchBy) params = params.set('SearchBy', data.SearchBy);
    if (data.SortBy) params = params.set('sortBy', data.SortBy);
    if (data.SortDirection) params = params.set('SortDirection', data.SortDirection);
    if (data.BookingStatus) params = params.set('BookingStatus', data.BookingStatus);
    if (data.SeatingCapacity) params = params.set('SeatingCapacity', data.SeatingCapacity);

    return this.http.get<PagedResult<MyRentals>>(`${environment.apiUrl}/Customer/GetMyRentals`, {
      params,
    });
  }

  cancelRentals(data: cancelRentalDto) {
    return this.http.put(`${environment.apiUrl}/Customer/CancelBooking`, data);
  }

  bookVehicle(data: bookVehicleDto) {
    return this.http.post(`${environment.apiUrl}/Customer/BookVehicle`, data);
  }

  getExtraServices() {
    return this.http.get<ExtraService[]>(`${environment.apiUrl}/Customer/GetExtraServices`);
  }
}
