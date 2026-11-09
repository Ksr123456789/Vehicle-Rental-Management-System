export interface PagedResult<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
}

export interface RentalCompany {
  id: number;
  companyName: string;
  companyType: number;
  ownerName: string;
  phoneNumber: string;
  headquartersLocation: string;
  licenseNumber: string;
  status: number;
}

export interface Vehicle {
  id: number;
  makeAndModel: string;
  licensePlate: string;
  registrationNumber: string;
  yearofManufacture: number;
  seatingCapacity: number;
  dailyRentalRate: number;
  vehicleAvailabilityStatus: number;
  mileage: number;
  vehicleCategoryId: number;
  rentalCompanyId: number;
  rentalCompanyName: string;
}

export interface AvailableVehicle {
  id: number;
  makeAndModel: string;
  licensePlate: string;
  registrationNumber: string;
  yearofManufacture: number;
  seatingCapacity: number;
  dailyRentalRate: number;
  mileage: number;
  companyName: string;
  categoryName: string;
  companyPhone: string;
}

export interface Booking {
  id: number;
  pickupDateTime: string;
  returnDateTime: string;
  pickupLocation: string;
  returnLocation: string;
  bookingStatus: number;
  totalDays: number;
  totalAmount: number;
  bookedOn: string;
  companyName: string;
  vehicleModel: string;
}

export interface MyRentals {
  id: number;
  makeModal: string;
  bookingStatus: number;
  seatingCapacity: number;
  pickupDateTime: string;
  returnDateTime: string;
  totalDays: number;
  totalAmount: number;
  additionalServiceAmount: number;
  rentalAmount: number;
  licensePlate: string;
  registrationNumber: string;
  yearofManufacture: number;
  dailyRentalRate: number;
  mileage: number;
}

export interface VehicleCategory {
  id: number;
  categoryName: string;
}

export interface getRentalCompanyDto {
  PageNumber: number;
  PageSize: number;
  SortBy: string;
  SortDirection: string;
  SearchBy: string;
  CompanyType: string;
  CompanyStatus: string;
}

export interface getVehicleDto {
  PageNumber: number;
  PageSize: number;
  SortBy: string;
  SortDirection: string;
  SearchBy: string;
  VehicleCategoryId: string;
  VehicleAvailabilityStatus: string;
}

export interface getBookingDto {
  PageNumber: number;
  PageSize: number;
  SortBy: string;
  SortDirection: string;
  SearchBy: string;
  BookingStatus: string;
  RentalCompanyId: string;
  ToDate: string;
  FromDate: string;
}

export interface getAvailableVehicleDto {
  PageNumber: number;
  PageSize: number;
  SortBy: string;
  SortDirection: string;
  SearchBy: string;
  VehicleCategoryId: string;
  SeatingCapacity: string;
  RentalCompanyId: string;
  FromDailyRentalRate: number;
  ToDailyRentalRate: number;
}

export interface getMyRentalDto {
  PageNumber: number;
  PageSize: number;
  SortBy: string;
  SortDirection: string;
  SearchBy: string;
  BookingStatus: string;
  SeatingCapacity: string;
}

export interface addCompanyDto {
  CompanyName: string;
  CompanyType: number;
  OwnerName: string;
  PhoneNumber: string;
  HeadquartersLocation: string;
  LicenseNumber: string;
  Status: number;
}

export interface updateCompanyDto {
  CompanyId: number;
  CompanyName: string;
  CompanyType: number;
  OwnerName: string;
  PhoneNumber: string;
  HeadquartersLocation: string;
  LicenseNumber: string;
  Status: number;
}

export interface addVehicleDto {
  MakeAndModel: string;
  LicensePlate: string;
  RegistrationNumber: string;
  YearofManufacture: number;
  SeatingCapacity: number;
  DailyRentalRate: number;
  VehicleAvailabilityStatus: number;
  Mileage: number;
  RentalCompanyId: number;
  VehicleCategoryId: number;
}

export interface updateVehicleDto {
  VehicleId: number;
  MakeAndModel: string;
  LicensePlate: string;
  RegistrationNumber: string;
  YearofManufacture: number;
  SeatingCapacity: number;
  DailyRentalRate: number;
  VehicleAvailabilityStatus: number;
  Mileage: number;
  RentalCompanyId: number;
  VehicleCategoryId: number;
}

export interface updateBookingStatusDto {
  BookingId: number | undefined;
  BookingStatus: number;
}

export interface cancelRentalDto {
  BookingId: number | undefined;
}

export interface bookVehicleDto {
  PickUpDateTime: string;
  ReturnDateTime: string;
  PickupLocation: string;
  ReturnLocation: string;
  VehicleId: number | undefined;
  Insurance: boolean;
  ExtraServiceIds: number[];
}

export interface ExtraService {
  id: number;
  name: string;
  description: string;
  price: number;
}

export interface bookingDashBoardDto {
  totalBookings: number;
  totalRevenue: number;
  fleetUtilizationRate: number;
}

export interface customerDetailDto {
  email: string;
  licenseNumber: string;
}

export interface getBookingDashboardDto {
  RentalCompanyId: string;
}
