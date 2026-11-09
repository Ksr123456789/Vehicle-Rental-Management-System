import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  addCompanyDto,
  addVehicleDto,
  Booking,
  bookingDashBoardDto,
  getBookingDashboardDto,
  getBookingDto,
  getRentalCompanyDto,
  getVehicleDto,
  PagedResult,
  RentalCompany,
  updateBookingStatusDto,
  updateCompanyDto,
  updateVehicleDto,
  Vehicle,
  VehicleCategory,
} from '../../Enviroment/model';
import { environment } from '../../Enviroment/environment';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  http = inject(HttpClient);

  getPagedRentalCompany(data: getRentalCompanyDto) {
    let params = new HttpParams();

    params = params.set('PageNumber', data.PageNumber.toString());
    params = params.set('PageSize', data.PageSize.toString());

    if (data.SearchBy) params = params.set('SearchBy', data.SearchBy);
    if (data.SortBy) params = params.set('sortBy', data.SortBy);
    if (data.SortDirection) params = params.set('SortDirection', data.SortDirection);
    if (data.CompanyType) params = params.set('CompanyType', data.CompanyType);
    if (data.CompanyStatus) params = params.set('CompanyStatus', data.CompanyStatus);

    return this.http.get<PagedResult<RentalCompany>>(
      `${environment.apiUrl}/Admin/GetPagedRentalCompany`,
      { params },
    );
  }

  getPagedVehicle(data: getVehicleDto) {
    let params = new HttpParams();

    params = params.set('PageNumber', data.PageNumber.toString());
    params = params.set('PageSize', data.PageSize.toString());

    if (data.SearchBy) params = params.set('SearchBy', data.SearchBy);
    if (data.SortBy) params = params.set('sortBy', data.SortBy);
    if (data.SortDirection) params = params.set('SortDirection', data.SortDirection);
    if (data.VehicleCategoryId) params = params.set('VehicleCategoryId', data.VehicleCategoryId);
    if (data.VehicleAvailabilityStatus)
      params = params.set('VehicleAvailabilityStatus', data.VehicleAvailabilityStatus);

    return this.http.get<PagedResult<Vehicle>>(`${environment.apiUrl}/Admin/GetPagedVehicle`, {
      params,
    });
  }

  getPagedBookings(data: getBookingDto) {
    let params = new HttpParams();

    params = params.set('PageNumber', data.PageNumber.toString());
    params = params.set('PageSize', data.PageSize.toString());

    if (data.SearchBy) params = params.set('SearchBy', data.SearchBy);
    if (data.SortBy) params = params.set('sortBy', data.SortBy);
    if (data.SortDirection) params = params.set('SortDirection', data.SortDirection);
    if (data.BookingStatus) params = params.set('BookingStatus', data.BookingStatus);
    if (data.RentalCompanyId) params = params.set('RentalCompanyId', data.RentalCompanyId);
    if (data.ToDate) params = params.set('ToDate', data.ToDate);
    if (data.FromDate) params = params.set('FromDate', data.FromDate);
    return this.http.get<PagedResult<Booking>>(`${environment.apiUrl}/Admin/GetPagedBooking`, {
      params,
    });
  }

  getVehicleCategory() {
    return this.http.get<VehicleCategory[]>(`${environment.apiUrl}/Admin/GetAllVehicleCategory`);
  }

  getRentalCompany() {
    return this.http.get<RentalCompany[]>(`${environment.apiUrl}/Admin/GetAllRentalCompany`);
  }

  addCompany(data: addCompanyDto) {
    return this.http.post(`${environment.apiUrl}/Admin/AddRentalCompany`, data);
  }

  updateCompany(data: updateCompanyDto) {
    return this.http.put(`${environment.apiUrl}/Admin/UpdateRentalCompany`, data);
  }

  deleteCompany(CompanyId: number) {
    let params = new HttpParams();
    params = params.set('CompanyId', CompanyId.toString());
    return this.http.delete(`${environment.apiUrl}/Admin/DeleteRentalCompany`, { params });
  }

  addVehicle(data: addVehicleDto) {
    return this.http.post(`${environment.apiUrl}/Admin/AddVehicle`, data);
  }

  updateVehicle(data: updateVehicleDto) {
    return this.http.put(`${environment.apiUrl}/Admin/UpdateVehicle`, data);
  }

  deleteVehicle(VehicleId: number) {
    let params = new HttpParams();
    params = params.set('VehicleId', VehicleId.toString());
    return this.http.delete(`${environment.apiUrl}/Admin/DeleteVehicle`, { params });
  }

  updateBookingStatus(data: updateBookingStatusDto) {
    return this.http.put(`${environment.apiUrl}/Admin/UpdateBookingStatus`, data);
  }

  getBookingDashBoard(data : getBookingDashboardDto) {

    let params = new HttpParams();

    

    if (data.RentalCompanyId) params = params.set('RentalCompanyId', data.RentalCompanyId);

    return this.http.get<bookingDashBoardDto>(`${environment.apiUrl}/Admin/GetBookingDashboard`, {params});
  }
}
