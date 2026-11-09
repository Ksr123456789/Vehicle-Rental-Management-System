using RentalManagementSystem.Application.Features.Customer.Command.BookVehicle;
using RentalManagementSystem.Application.Features.Customer.Command.CancelBooking;
using RentalManagementSystem.Application.Features.Customer.Query.GetAllExtraServices;
using RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle;
using RentalManagementSystem.Application.Features.Customer.Query.GetRentalCompany;
using RentalManagementSystem.Application.Features.Customer.Query.GetVehicleCategory;
using RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;

namespace RentalManagementSystem.Application.ServiceContracts
{
    public interface ICustomerService
    {
        Task<PagedResult<GetAvailablePagedVehicleQueryResponse>> GetAvailablePagedVehicle(GetAvailablePagedVehicleQuery query);
        Task<BookVehicleCommandResponse> BookVehicle(BookVehicleCommand command);
        Task<CancelBookingCommandResponse> CancelBooking(CancelBookingCommand command);
        Task<PagedResult<MyPagedRentalsQueryResponse>> MyPagedRentals(MyPagedRentalsQuery query);
        Task<List<RentalCompany>> GetRentalCompany(GetRentalCompanyQuery query);
        Task<List<VehicleCategory>> GetVehicleCategory(GetVehicleCategoryQuery query);
        Task<List<ExtraService>> GetAllExtraServices(GetAllExtraServicesQuery query);


    }
}
