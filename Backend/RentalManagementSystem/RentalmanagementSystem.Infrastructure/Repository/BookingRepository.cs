using Microsoft.EntityFrameworkCore;
using RentalmanagementSystem.Infrastructure.DbContext;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using RentalManagementSystem.Domain.RepositoryContracts;

namespace RentalmanagementSystem.Infrastructure.Repository
{
    public class BookingRepository(AppDbContext context) : IBookingRepository
    {
        public async Task BookVehicleAsync(Booking newBooking, List<int> ExtraServiceIds)
        {

            

            await context.Bookings.AddAsync(newBooking);
            await context.SaveChangesAsync();

            int bookingId = newBooking.Id;

            var services = await context.ExtraServices.Where(x => ExtraServiceIds.Contains(x.Id)).ToListAsync();

            var bookingServices = services.Select(service => new BookingExtraService
            {
                BookingId = bookingId,
                ExtraServiceId = service.Id,
                Price = service.Price,
                
            }).ToList();

            foreach(var service in bookingServices)
            {
                newBooking.TotalAmount += service.Price;
                newBooking.AdditionalServiceAmount += service.Price;
            }

            context.BookingExtraServices.AddRange(bookingServices);
            await context.SaveChangesAsync();

        }

        public async Task<List<Booking>> GetCustomerBookings(Guid CustomerId)
        {
            return await context.Bookings.Where(x => x.CustomerId == CustomerId).ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int BookingId)
        {
            return await context.Bookings.FirstOrDefaultAsync(x => x.Id == BookingId);
        }

        public async Task CancelBookingAsync(int BookingId, bool cancelWithinOneDay)
        {
            var booking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == BookingId);
            if(booking != null)
            {
                if (cancelWithinOneDay) booking.TotalAmount = booking.TotalAmount / 2;
                booking.BookingStatus = BookingStatus.Cancelled;
                await context.SaveChangesAsync();
            }
        }

          public async Task<PagedResult<Booking>> MyPagedRentalsAsync(MyRentalFilterParameters query)
        {
            var booking = context.Bookings.Include(x => x.Vehicle).Where(x => x.CustomerId == query.CustomerId);

            if (query.BookingStatus.HasValue)
            {
                booking = booking.Where(x => x.BookingStatus == query.BookingStatus);
            }

            if (query.SeatingCapacity.HasValue)
            {
                booking = booking.Where(x => x.Vehicle.SeatingCapacity == query.SeatingCapacity);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchBy))
            {
                var search = query.SearchBy.Trim();

                booking = booking.Where(x => x.PickupDateTime.ToString().Contains(search) ||
                                                         x.PickupDateTime.ToString().Contains(search) ||
                                                         x.ReturnDateTime.ToString().Contains(search) ||
                                                         x.TotalAmount.ToString().Contains(search) ||
                                                         x.BookingStatus.ToString().Contains(search) ||
                                                         x.Vehicle.MakeAndModel.Contains(search) ||
                                                         x.Vehicle.SeatingCapacity.ToString().Contains(search)
                                                         );
            }

            booking = booking.OrderByDescending(x => x.Id);

            var totalCount = await booking.CountAsync();

            var items = await booking
                              .Skip((query.PageNumber - 1) * query.PageSize)
                              .Take(query.PageSize)
                              .ToListAsync();

            return new PagedResult<Booking>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };

           
        }
        public async Task<PagedResult<Booking>> GetPagedBooking(BookingFilterParameters query) { 
            var booking = context.Bookings.Include(x=>x.Vehicle).ThenInclude(x=>x.RentalCompany).AsQueryable();

            if (query.BookingStatus.HasValue)
            {
                booking = booking.Where(x => x.BookingStatus == query.BookingStatus);
            }

            if (query.RentalCompanyId.HasValue)
            {
                booking = booking.Where(x => x.Vehicle.RentalCompanyId == query.RentalCompanyId);
            }

            if (query.FromDate.HasValue && query.ToDate.HasValue)
            {
                booking = booking.Where(x => x.BookedOn >= query.FromDate.Value.Date && x.BookedOn <= query.ToDate.Value.Date);
            }
            else if (query.FromDate.HasValue )
            {
                booking = booking.Where(x => x.BookedOn >= query.FromDate.Value.Date );
            }
            if (query.ToDate.HasValue)
            {
                booking = booking.Where(x =>  x.BookedOn <= query.ToDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchBy))
            {
                var search = query.SearchBy.Trim();

                booking = booking.Where(x => x.PickupDateTime.ToString().Contains(search) ||
                                                         x.PickupDateTime.ToString().Contains(search) ||
                                                         x.ReturnDateTime.ToString().Contains(search) ||
                                                         x.TotalAmount.ToString().Contains(search) ||
                                                         x.BookingStatus.ToString().Contains(search) ||
                                                         x.PickupLocation.Contains(search) ||
                                                         x.ReturnLocation.Contains(search) ||
                                                         x.TotalDays.ToString().Contains(search) ||
                                                         x.TotalAmount.ToString().Contains(search) ||
                                                         x.BookedOn.ToString().Contains(search)
                                                         );
            }


            booking = query.SortBy?.ToLower() switch
            {
                "id" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.Id) :
                booking.OrderBy(x => x.Id),
                "pickupdatetime" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.PickupDateTime) :
                booking.OrderBy(x => x.PickupDateTime),
                "returndatetime" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.ReturnDateTime.ToString()) :
                booking.OrderBy(x => x.ReturnDateTime.ToString()),
                "pickuplocation" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.PickupLocation) :
                booking.OrderBy(x => x.PickupLocation),
                "returnlocation" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.ReturnLocation) :
                booking.OrderBy(x => x.ReturnLocation),
                "bookingstatus" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.BookingStatus) :
                booking.OrderBy(x => x.BookingStatus),
                "totaldays" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.TotalDays) :
                booking.OrderBy(x => x.TotalDays),
                "totalamount" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.TotalAmount) :
                booking.OrderBy(x => x.TotalAmount),
                "bookedon" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.BookedOn) :
                booking.OrderBy(x => x.BookedOn),
                "companyname" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.Vehicle.RentalCompany.CompanyName) :
                booking.OrderBy(x => x.Vehicle.RentalCompany.CompanyName),
                "vehiclemodel" => query.SortDirection == "desc" ? booking.OrderByDescending(x => x.Vehicle.MakeAndModel) :
                booking.OrderBy(x => x.Vehicle.MakeAndModel),
                _ => booking.OrderByDescending(x => x.Id)
            };

            

            var totalCount = await booking.CountAsync();

            var items = await booking
                              .Skip((query.PageNumber - 1) * query.PageSize)
                              .Take(query.PageSize)
                              .ToListAsync();

            return new PagedResult<Booking>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };


        }
        public async Task<List<Booking>> GetBookingsByVehicleId(int vehicleId)
        {
            return await context.Bookings.Where(x => x.VehicleId == vehicleId).ToListAsync();
        }
        public async Task UpdateBookingStatus(int BookingId, BookingStatus BookingStatus)
        {
            var booking = await GetBookingByIdAsync(BookingId);

            if (booking != null)
            {
                if (booking.BookingStatus == BookingStatus.Cancelled) booking.TotalAmount = 0;
                booking.BookingStatus = BookingStatus;
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<ExtraService>> GetAllExtraServicesAsync()
        {
            return await context.ExtraServices.ToListAsync();
        }
        public async Task<BookingDashBoardDto> GetBookingDashboardAsync(int? RentalCompanyId)
        {

            int totalBookings = 0;
            decimal totalRevenue = 0;

            if (RentalCompanyId.HasValue)
            {

               

                var vehicles = await context.Vehicles.Include(x => x.Bookings).Where(x => x.RentalCompanyId == RentalCompanyId).ToListAsync();

                var availableVehicles = vehicles.Count(x => x.VehicleAvailabilityStatus == VehicleAvailabilityStatus.Available);
                var activeVehicles = 0;
                var now = DateTime.Now;

                foreach(var vehicle in vehicles)
                {
                    foreach(var booking in vehicle.Bookings)
                    {
                        if(booking.PickupDateTime.Date<= now.Date && booking.ReturnDateTime.Date > now.Date && booking.BookingStatus != BookingStatus.Cancelled)
                        {
                            
                            activeVehicles++;
                        }
                        totalRevenue += booking.TotalAmount;
                        totalBookings++;

                    }
                }  

                if(availableVehicles == 0)
                {
                    return new BookingDashBoardDto
                    {
                        TotalBookings = totalBookings,
                        TotalRevenue = totalRevenue,
                        FleetUtilizationRate = 0
                    };
                }
                else
                {
                    return new BookingDashBoardDto
                    {
                        TotalBookings = totalBookings,
                        TotalRevenue = totalRevenue,
                        FleetUtilizationRate = (decimal)((activeVehicles) / (availableVehicles)) * 100
                    };
                }
            }

            var bookings = await context.Bookings.ToListAsync();

            foreach (var booking in bookings)
            {
                totalRevenue += booking.TotalAmount;
            }
            return new BookingDashBoardDto
            {
                TotalBookings = totalBookings,
                TotalRevenue = totalRevenue,
                FleetUtilizationRate = 0
            };
            
        }
    }
}

