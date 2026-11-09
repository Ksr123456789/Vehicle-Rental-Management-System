using Microsoft.AspNetCore.Identity;
using RentalManagementSystem.Application.Features.Customer.Command.BookVehicle;
using RentalManagementSystem.Application.Features.Customer.Command.CancelBooking;
using RentalManagementSystem.Application.Features.Customer.Query.GetAllExtraServices;
using RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle;
using RentalManagementSystem.Application.Features.Customer.Query.GetRentalCompany;
using RentalManagementSystem.Application.Features.Customer.Query.GetVehicleCategory;
using RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using RentalManagementSystem.Domain.RepositoryContracts;

namespace RentalManagementSystem.Application.Services
{
    public class CustomerService(IVehicleRepository vehicleRepository,IRentalCompanyRepository rentalCompanyRepository ,IBookingRepository bookingRepository , ICurrentUser currentUser, UserManager<ApplicationUser> userManager) : ICustomerService
    {

        public async Task<PagedResult<GetAvailablePagedVehicleQueryResponse>> GetAvailablePagedVehicle(GetAvailablePagedVehicleQuery query)
        {

            var queryData = new AvailableVehicleFilterParameters
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortDirection = query.SortDirection,
                SortBy = query.SortBy,
                SeatingCapacity = query.SeatingCapacity,
                RentalCompanyId = query.RentalCompanyId,
                FromDailyRentalRate = query.FromDailyRentalRate,
                ToDailyRentalRate   = query.ToDailyRentalRate
            };

           var result = await vehicleRepository.GetAllAvailableVehicle(queryData);


            return new PagedResult<GetAvailablePagedVehicleQueryResponse>
            {
                Items = result.Items.Select(x => new GetAvailablePagedVehicleQueryResponse
                {

                    Id = x.Id,
                    MakeAndModel = x.MakeAndModel,
                    LicensePlate = x.LicensePlate,
                    RegistrationNumber = x.RegistrationNumber,
                    YearofManufacture = x.YearofManufacture,
                    SeatingCapacity = x.SeatingCapacity,
                    DailyRentalRate = x.DailyRentalRate,
                    Mileage = x.Mileage,
                    CompanyName = x.RentalCompany.CompanyName,
                    CategoryName = x.VehicleCategory.CategoryName,
                    CompanyPhone = x.RentalCompany.PhoneNumber,

                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
            };

        }

        public async Task<BookVehicleCommandResponse> BookVehicle(BookVehicleCommand command)
        {


            if (command.PickUpDateTime < DateTime.UtcNow)
            {
                
                throw new Exception("pickup date cannot be in past");
            }

            if(command.PickUpDateTime >= command.ReturnDateTime)
            {
                throw new Exception("Pickup Date cannot be lesser than Return Date");
            }
      

            if (Guid.TryParse(currentUser.UserId, out Guid customerId))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }



            var customerBookings = await bookingRepository.GetCustomerBookings(customerId);

            
            int activeBookings = 0;
            bool sameVehicleBookedTwice = false;
            bool bookingAlreadyExistForTimePeriod = false;


            foreach (var booking in customerBookings)
            {
                if((booking.BookingStatus == BookingStatus.Pending || booking.BookingStatus == BookingStatus.Confirmed || booking.BookingStatus == BookingStatus.Active)) activeBookings++;
                if((booking.VehicleId == command.VehicleId) && (booking.BookingStatus == BookingStatus.Pending) && ((command.PickUpDateTime <= booking.ReturnDateTime) && (booking.PickupDateTime <= command.ReturnDateTime)))
                {
                    sameVehicleBookedTwice = true;
                    break;
                }

                if((booking.BookingStatus != BookingStatus.Completed && booking.BookingStatus != BookingStatus.Cancelled) && ((command.PickUpDateTime <= booking.ReturnDateTime) && (booking.PickupDateTime <= command.ReturnDateTime)))
                {
                     bookingAlreadyExistForTimePeriod = true;
                }

            }

            if (sameVehicleBookedTwice) throw new Exception("Customer cannot book same vehicle twice with overlapping dates while status is Pending");
            
            if (activeBookings >= 5)
            {
                throw new Exception("cannot have more than five active bookings");
            }
            if (bookingAlreadyExistForTimePeriod) throw new Exception("customer has already booked another vehicle given time period");


            var vehicle = await vehicleRepository.GetVehicleById(command.VehicleId);

            if (vehicle == null) throw new Exception("vehicle not found");


            var vehicleBookings = await bookingRepository.GetBookingsByVehicleId(command.VehicleId);

            

            foreach (var booking in vehicleBookings)
            {
                if((booking.BookingStatus == BookingStatus.Pending || booking.BookingStatus == BookingStatus.Confirmed || booking.BookingStatus == BookingStatus.Active) 
                    && (command.PickUpDateTime <= booking.ReturnDateTime) && (booking.PickupDateTime <= command.ReturnDateTime))
                {
                    bookingAlreadyExistForTimePeriod = true;
                    break;
                }
                

            }

            if (bookingAlreadyExistForTimePeriod) throw new Exception("vehicle is already booked for given time period");

            

            var user = await userManager.FindByIdAsync(currentUser.UserId);

            if (user == null) throw new Exception("user not found");

            if(user.LicenseExpiryDate < command.ReturnDateTime)
            {
                throw new Exception("license expire before return date");
            }



            int TotalDays = (int)(((command.ReturnDateTime - command.PickUpDateTime).TotalDays) + 1);

            

            var newBooking = new Booking
            {
                PickupDateTime = command.PickUpDateTime,
                ReturnDateTime = command.ReturnDateTime,
                PickupLocation = command.PickupLocation,
                ReturnLocation = command.ReturnLocation,
                VehicleId = command.VehicleId,
                Insurance = command.Insurance,
                CustomerId = customerId,
                BookedOn = DateTime.Now,
                BookingStatus = BookingStatus.Pending,
                TotalDays = TotalDays,
                TotalAmount = TotalDays * vehicle.DailyRentalRate,
                RentalAmount = TotalDays * vehicle.DailyRentalRate
            };

            await bookingRepository.BookVehicleAsync(newBooking, command.ExtraServiceIds);
            return new BookVehicleCommandResponse { };
        }

        public async Task<CancelBookingCommandResponse> CancelBooking(CancelBookingCommand command)
        {
            var booking = await bookingRepository.GetBookingByIdAsync(command.BookingId);

            if (booking == null) throw new Exception("booking not found");

            if(booking.BookingStatus != BookingStatus.Pending)
            {
                throw new Exception("only pending booking can be cancelled");
            }

            bool cancelWithinOneDay = false;

            if(booking.BookedOn > DateTime.UtcNow.AddHours(-24))
            {
                cancelWithinOneDay = true;
            }

            await bookingRepository.CancelBookingAsync(command.BookingId, cancelWithinOneDay);
            return new CancelBookingCommandResponse{ };
        }

        public async Task<PagedResult<MyPagedRentalsQueryResponse>> MyPagedRentals(MyPagedRentalsQuery query)
        {

            if (Guid.TryParse(currentUser.UserId, out Guid customerId))
            {
                Console.WriteLine("current user id accessed successfully");
            }
            else
            {
                throw new Exception("can't access current user id");
            }

            var queryData = new MyRentalFilterParameters
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortBy = query.SortBy,
                SortDirection = query.SortDirection,
                SearchBy = query.SearchBy,
                BookingStatus = query.BookingStatus,
                SeatingCapacity = query.SeatingCapacity,
                CustomerId = customerId,
            };

            var result = await bookingRepository.MyPagedRentalsAsync(queryData); 


            return new PagedResult<MyPagedRentalsQueryResponse>
            {
                Items = result.Items.Select( x => new MyPagedRentalsQueryResponse
                {
                    Id = x.Id,
                    MakeModal = x.Vehicle.MakeAndModel,
                    SeatingCapacity = x.Vehicle.SeatingCapacity,
                    PickupDateTime = x.PickupDateTime,
                    ReturnDateTime = x.ReturnDateTime,
                    BookingStatus = x.BookingStatus,
                    AdditionalServiceAmount = x.AdditionalServiceAmount,
                    RentalAmount = x.RentalAmount,
                    TotalDays = x.TotalDays,
                    TotalAmount = x.TotalAmount,
                    LicensePlate = x.Vehicle.LicensePlate,
                    RegistrationNumber = x.Vehicle.RegistrationNumber,
                    YearofManufacture = x.Vehicle.YearofManufacture,
                    DailyRentalRate = x.Vehicle.DailyRentalRate,
                    Mileage = x.Vehicle.Mileage
                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
            };

        }
        public async Task<List<RentalCompany>> GetRentalCompany(GetRentalCompanyQuery query)
        {
            return await rentalCompanyRepository.GetAllRentalCompanyAsync();
        }
        public async Task<List<VehicleCategory>> GetVehicleCategory(GetVehicleCategoryQuery query)
        {
            return await vehicleRepository.GetAllVehicleCategoryAsync();
        }
        public async Task<List<ExtraService>> GetAllExtraServices(GetAllExtraServicesQuery query)
        {
            return await bookingRepository.GetAllExtraServicesAsync();
        }
       

    }
}

