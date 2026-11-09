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
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;

namespace RentalManagementSystem.Application.ServiceContracts
{
    public interface IAdminService
    {

        Task<AddRentalCompanyCommandResponse> AddRentalCompany(AddRentalCompanyCommand command);
        Task<UpdateRentalCompanyCommandResponse> UpdateRentalCompany(UpdateRentalCompanyCommand command);
        Task<PagedResult<GetPagedRentalCompanyQueryResponse>> GetPagedRentalCompany(GetPagedRentalCompanyQuery query);
        Task<List<RentalCompany>> GetAllRentalCompany(GetAllRentalCompanyQuery query);
        Task<DeleteCompanyCommandResponse> DeleteRentalCompany(DeleteCompanyCommand command);
        Task<List<VehicleCategory>> GetAllVehicleCategory(GetAllVehicleCategoryQuery query);
        Task<AddVehicleCommandResponse> AddVehicle(AddVehicleCommand command);
        Task<PagedResult<GetPagedVehicleQueryResponse>> GetPagedVehicle(GetPagedVehicleQuery query);
        Task<DeleteVehicleCommandResponse> DeleteVehicle(DeleteVehicleCommand command);
        Task<UpdateVehicleCommandResponse> UpdateVehicle(UpdateVehicleCommand command);
        Task<PagedResult<GetPagedBookingQueryResponse>> GetPagedBooking(GetPagedBookingQuery query);
        Task<UpdateBookingStatusCommandResponse> UpdateBookingStatus(UpdateBookingStatusCommand command);
        Task<BookingDashBoardDto> GetBookingDashBoardDetails(GetBookingDashBoardQuery query);

    }
}
