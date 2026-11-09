using RentalManagementSystem.Application.Features.Admin.command.AddRentalCompany;
using RentalManagementSystem.Application.Features.Admin.command.AddVehicle;
using RentalManagementSystem.Application.Features.Admin.command.DeleteCompany;
using RentalManagementSystem.Application.Features.Admin.command.DeleteVehicle;
using RentalManagementSystem.Application.Features.Admin.command.UpdateBookingStatus;
using RentalManagementSystem.Application.Features.Admin.command.UpdateRentalCompany;
using RentalManagementSystem.Application.Features.Admin.command.UpdateVehicle;
using RentalManagementSystem.Application.Features.Admin.query.GetAllRentalCompany;
using RentalManagementSystem.Application.Features.Admin.query.GetAllVehicleCategory;
using RentalManagementSystem.Application.Features.Admin.query.GetBookingDashBoard;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedBooking;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedRentalCompany;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedVehicle;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using RentalManagementSystem.Domain.RepositoryContracts;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.NetworkInformation;

namespace RentalManagementSystem.Application.Services
{
    public class AdminService(IRentalCompanyRepository rentalCompanyRepository ,IVehicleRepository vehicleRepository, IBookingRepository bookingRepository ,ICurrentUser currentUser) : IAdminService
    {

        public async Task<AddRentalCompanyCommandResponse> AddRentalCompany(AddRentalCompanyCommand command)
        {

            bool licenseNumberExist = await rentalCompanyRepository.LicenseNumberExistAsync(command.LicenseNumber);

            if (licenseNumberExist)
            {
                throw new Exception("License number of company must be unique");
            }

            bool companyNameExist = await rentalCompanyRepository.CompanyNameExistAsync(command.CompanyName.Trim());

            if (companyNameExist) throw new Exception("company name already exists");

            if(Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }


            var rentalCompany = new RentalCompany
            {
                CompanyName = command.CompanyName,
                CompanyType = command.CompanyType,
                OwnerName = command.OwnerName,
                PhoneNumber = command.PhoneNumber,
                HeadquartersLocation = command.HeadquartersLocation,
                LicenseNumber = command.LicenseNumber,
                Status = command.Status,
                IsDeleted = false,
                CreatedBy = user,
                CreatedDate = DateTime.Now,

            };

            await rentalCompanyRepository.AddRentalCompanyAsync(rentalCompany);
            return new AddRentalCompanyCommandResponse { };
        }
        public async Task<UpdateRentalCompanyCommandResponse> UpdateRentalCompany(UpdateRentalCompanyCommand command)
        {
            var RentalCompany = await rentalCompanyRepository.RentalCompanyExistsAsync(command.CompanyId);
            if(RentalCompany == null)
            {
                throw new Exception("rental company not found");
            }

            if (Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            bool notUniqueLicense =  await rentalCompanyRepository.UniqueLicenseOnUpdate(command.CompanyId, command.LicenseNumber);

            if (notUniqueLicense) throw new Exception("license number should be unique");

            bool notUniqueCompanyName = await rentalCompanyRepository.uniqueCompanyNameOnUpdate(command.CompanyId, command.CompanyName.Trim());

            if(notUniqueCompanyName) throw new Exception("already have a company with this name");


           
                RentalCompany.CompanyName = command.CompanyName;
                RentalCompany.CompanyType = command.CompanyType;
                RentalCompany.OwnerName = command.OwnerName;
                RentalCompany.PhoneNumber = command.PhoneNumber;
                RentalCompany.LicenseNumber = command.LicenseNumber;
                RentalCompany.HeadquartersLocation = command.HeadquartersLocation;
                RentalCompany.Status = command.Status;
                RentalCompany.UpdatedBy = user;
                RentalCompany.UpdatedDate = DateTime.Now;

             

            await rentalCompanyRepository.UpdateRentalCompanyAsync(RentalCompany);

            return  new UpdateRentalCompanyCommandResponse { };
        }
        public async Task<PagedResult<GetPagedRentalCompanyQueryResponse>> GetPagedRentalCompany(GetPagedRentalCompanyQuery query)
        {


            var queryData = new RentalCompanyFilterParameters
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortBy = query.SortBy,
                SortDirection = query.SortDirection,
                SearchBy = query.SearchBy,
                CompanyStatus = query.CompanyStatus,
                CompanyType = query.CompanyType,
            };


            var result = await rentalCompanyRepository.GetPagedRentalCompanyAsync(queryData);


            return new PagedResult<GetPagedRentalCompanyQueryResponse>
            {
                Items = result.Items.Select(x => new GetPagedRentalCompanyQueryResponse
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    CompanyType = x.CompanyType,
                    OwnerName = x.OwnerName,
                    PhoneNumber = x.PhoneNumber,
                    HeadquartersLocation = x.HeadquartersLocation,
                    LicenseNumber = x.LicenseNumber,
                    Status = x.Status,
                }).ToList(),

                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
            };


        }
        public async Task<List<RentalCompany>> GetAllRentalCompany(GetAllRentalCompanyQuery query)
        {

            return await rentalCompanyRepository.GetAllRentalCompanyAsync();

        }
        public async Task<DeleteCompanyCommandResponse> DeleteRentalCompany(DeleteCompanyCommand command)
        {


            var RentalCompany = await rentalCompanyRepository.GetRentalCompanyById(command.CompanyId);

            if(RentalCompany == null)
            {
                throw new Exception("RentalCompany not found");
            }

            bool ActiveVehicle = RentalCompany.Vehicles.Any(x => !x.IsDeleted);
            

            if (ActiveVehicle) throw new Exception("cannot delete Rental Company with vehicle");


            if (Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            await rentalCompanyRepository.DeleteRentalCompanyAsync(command.CompanyId, user);

            return new DeleteCompanyCommandResponse { };
        }
        public async Task<List<VehicleCategory>> GetAllVehicleCategory(GetAllVehicleCategoryQuery query)
        {
            return await vehicleRepository.GetAllVehicleCategoryAsync();
        }
        public async Task<AddVehicleCommandResponse> AddVehicle(AddVehicleCommand command)
        {

            bool LicensePlate = await vehicleRepository.LicensePlateExistAsync(command.LicensePlate);

            if (LicensePlate)
            {
                throw new Exception("Vehicle with this license plate already exists");
            }
            bool RegistrationNumber = await vehicleRepository.RegistrationNumberExistAsync(command.RegistrationNumber);

            if (RegistrationNumber) throw new Exception("Vehicle with this registration number already exists");

            if (Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            var vehicle = new Vehicle
            {
                MakeAndModel = command.MakeAndModel,
                LicensePlate = command.LicensePlate,
                RegistrationNumber = command.RegistrationNumber,
                YearofManufacture = command.YearofManufacture,
                SeatingCapacity = command.SeatingCapacity,
                DailyRentalRate = command.DailyRentalRate,
                VehicleAvailabilityStatus = command.VehicleAvailabilityStatus,
                Mileage = command.Mileage,
                RentalCompanyId = command.RentalCompanyId,
                VehicleCategoryId = command.VehicleCategoryId,
                CreatedBy = user,
                CreatedDate = DateTime.Now,
            };

            
            await vehicleRepository.AddVehicleAsync(vehicle);

            return new AddVehicleCommandResponse { };
        }
        public async Task<PagedResult<GetPagedVehicleQueryResponse>> GetPagedVehicle(GetPagedVehicleQuery query)
        {

            var queryData = new VehicleFilterParameters
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortBy = query.SortBy,
                SortDirection = query.SortDirection,
                SearchBy = query.SearchBy,
                VehicleCategoryId = query.VehicleCategoryId,
                VehicleAvailabilityStatus = query.VehicleAvailabilityStatus
            };

            var result = await vehicleRepository.GetPagedVehicleAsync(queryData);

            return new PagedResult<GetPagedVehicleQueryResponse>
            {
                Items = result.Items.Select(x => new GetPagedVehicleQueryResponse
                {

                    Id = x.Id,
                    MakeAndModel = x.MakeAndModel,
                    LicensePlate = x.LicensePlate,
                    RegistrationNumber = x.RegistrationNumber,
                    YearofManufacture = x.YearofManufacture,
                    SeatingCapacity = x.SeatingCapacity,
                    DailyRentalRate = x.DailyRentalRate,
                    VehicleAvailabilityStatus = x.VehicleAvailabilityStatus,
                    Mileage = x.Mileage,
                    VehicleCategory = x.VehicleCategory.CategoryName,
                    RentalCompanyId = x.RentalCompanyId,
                    RentalCompanyName = x.RentalCompany.CompanyName,
                    VehicleCategoryId = x.VehicleCategoryId
                    
                    

                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
            };

        }
      
        public async Task<DeleteVehicleCommandResponse> DeleteVehicle(DeleteVehicleCommand command)
        {
            if (Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            var bookings =  await bookingRepository.GetBookingsByVehicleId(command.VehicleId);

            bool canDelete = true;
            foreach(var booking in bookings)
            {
                if (booking.BookingStatus == BookingStatus.Pending || booking.BookingStatus == BookingStatus.Confirmed || booking.BookingStatus == BookingStatus.Active) canDelete = false;
            }

            if (!canDelete) throw new Exception("cannot delete vehicle with ongoing booking");

            await vehicleRepository.DeleteVehicleAsync(command.VehicleId, user);

            return  new DeleteVehicleCommandResponse { };
        }
        public async Task<UpdateVehicleCommandResponse> UpdateVehicle(UpdateVehicleCommand command)
        {
            if (Guid.TryParse(currentUser.UserId, out Guid user))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            bool notUniqueLicense = await vehicleRepository.UniqueLicensePlateOnUpdate(command.VehicleId, command.LicensePlate.Trim());

            if (notUniqueLicense) throw new Exception("License plate should be unique");

            bool notUniqueRegistration = await vehicleRepository.UniqueRegistrationNumberOnUpdate(command.VehicleId, command.RegistrationNumber.Trim());

            if (notUniqueRegistration) throw new Exception("Registration number should be unique");

            var vehicle = await vehicleRepository.GetVehicleById(command.VehicleId);

            if (vehicle == null) throw new Exception("Vehicle not found");
            
                vehicle.MakeAndModel = command.MakeAndModel;
                vehicle.LicensePlate = command.LicensePlate;
                vehicle.RegistrationNumber = command.RegistrationNumber;
                vehicle.YearofManufacture = command.YearofManufacture;
                vehicle.SeatingCapacity = command.SeatingCapacity;
                vehicle.DailyRentalRate = command.DailyRentalRate;
                vehicle.VehicleAvailabilityStatus = command.VehicleAvailabilityStatus;
                vehicle.Mileage = command.Mileage;
                vehicle.RentalCompanyId = command.RentalCompanyId;
                vehicle.VehicleCategoryId = command.VehicleCategoryId;
                vehicle.UpdatedBy = user;
                vehicle.UpdatedDate = DateTime.Now;

            

            await vehicleRepository.UpdateVehicleAsync(vehicle);

            return new UpdateVehicleCommandResponse { };

        }
        public async Task<PagedResult<GetPagedBookingQueryResponse>> GetPagedBooking(GetPagedBookingQuery query)
        {

            var queryData = new BookingFilterParameters
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortBy = query.SortBy,
                SortDirection = query.SortDirection,
                SearchBy = query.SearchBy,
                BookingStatus = query.BookingStatus,
                RentalCompanyId = query.RentalCompanyId,
                FromDate = query.FromDate,
                ToDate = query.ToDate

            };

            var result =  await bookingRepository.GetPagedBooking(queryData);

            return new PagedResult<GetPagedBookingQueryResponse>
            {
                Items = result.Items.Select(x => new GetPagedBookingQueryResponse
                {
                    Id = x.Id,
                    PickupDateTime = x.PickupDateTime,
                    ReturnDateTime = x.ReturnDateTime,
                    PickupLocation = x.PickupLocation,
                    ReturnLocation  = x.ReturnLocation,
                    BookingStatus = x.BookingStatus,
                    TotalAmount = x.TotalAmount,
                    TotalDays = x.TotalDays,
                    BookedOn = x.BookedOn,
                    CompanyName = x.Vehicle.RentalCompany.CompanyName,
                    VehicleModel = x.Vehicle.MakeAndModel
                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
            };

        }
        public async Task<UpdateBookingStatusCommandResponse> UpdateBookingStatus(UpdateBookingStatusCommand command)
        {

           var booking = await bookingRepository.GetBookingByIdAsync(command.BookingId);

            if (booking == null) throw new Exception("booking not found");

            if(booking.BookingStatus == BookingStatus.Completed || booking.BookingStatus == BookingStatus.Cancelled)
            {
                throw new Exception("cannot update completed and cancelled bookings");
            }

            if(booking.BookingStatus == BookingStatus.Pending)
            {
                if (command.BookingStatus != BookingStatus.Confirmed && command.BookingStatus != BookingStatus.Cancelled) throw new Exception("pending status can be changed to confirm and cancel");
                await bookingRepository.UpdateBookingStatus(command.BookingId, command.BookingStatus);
            }
            
            else if(booking.BookingStatus == BookingStatus.Confirmed)
            {
                if (command.BookingStatus != BookingStatus.Active && command.BookingStatus != BookingStatus.Cancelled) throw new Exception("only confirmed status can be changed to active and cancel");

                if ((command.BookingStatus == BookingStatus.Active) && (DateTime.Today < booking.PickupDateTime.Date)) throw new Exception("Mark Active only on or after Rental Start Date");
                await bookingRepository.UpdateBookingStatus(command.BookingId, command.BookingStatus);
            }

            else if(booking.BookingStatus == BookingStatus.Active)
            {
                if (command.BookingStatus != BookingStatus.Completed ) throw new Exception("only active booking can be marked as completed");
                else if ((DateTime.UtcNow < booking.ReturnDateTime)) throw new Exception("can be marked completed only after return date and time");
            }

            await bookingRepository.UpdateBookingStatus(command.BookingId, command.BookingStatus);

            return new UpdateBookingStatusCommandResponse { };

        }
        public async Task<BookingDashBoardDto> GetBookingDashBoardDetails(GetBookingDashBoardQuery query)
        {
            return await bookingRepository.GetBookingDashboardAsync(query?.RentalCompanyId);
        }
    }
}

