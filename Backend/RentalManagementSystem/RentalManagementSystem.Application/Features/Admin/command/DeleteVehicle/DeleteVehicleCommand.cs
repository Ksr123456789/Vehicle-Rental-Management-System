using MediatR;

namespace RentalManagementSystem.Application.Features.Admin.command.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<DeleteVehicleCommandResponse>
    {
        public int VehicleId { get; set; }
    }
}
