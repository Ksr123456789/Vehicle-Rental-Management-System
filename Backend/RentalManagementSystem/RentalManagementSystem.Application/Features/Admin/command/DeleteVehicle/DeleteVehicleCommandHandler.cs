using MediatR;
using RentalManagementSystem.Application.ServiceContracts;

namespace RentalManagementSystem.Application.Features.Admin.command.DeleteVehicle
{
    public class DeleteVehicleCommandHandler(IAdminService adminService) : IRequestHandler<DeleteVehicleCommand, DeleteVehicleCommandResponse>
    {
        public async Task<DeleteVehicleCommandResponse> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            return await adminService.DeleteVehicle(request);
        }
    }
}
