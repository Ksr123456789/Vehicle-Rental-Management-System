using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using System.Globalization;

namespace RentalManagementSystem.Domain.RepositoryContracts
{
    public interface IBookingRepository
    {

        Task BookVehicleAsync(Booking newBooking, List<int>ExtraServiceIds);
        Task<List<Booking>> GetCustomerBookings(Guid CustomerId);
        Task<Booking?> GetBookingByIdAsync(int BookingId);
        Task CancelBookingAsync(int BookingId, bool cancelWithinOneDay);
        Task<PagedResult<Booking>> MyPagedRentalsAsync(MyRentalFilterParameters query);
        Task<List<Booking>> GetBookingsByVehicleId(int vehicleId);
        Task<PagedResult<Booking>> GetPagedBooking(BookingFilterParameters query);
        Task UpdateBookingStatus(int BookingId, BookingStatus BookingStatus);
        Task<List<ExtraService>> GetAllExtraServicesAsync();
        Task<BookingDashBoardDto> GetBookingDashboardAsync(int ? RentalCompanyId);
    }
}
