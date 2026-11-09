using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateVehicle
{
    public class UpdateVehicleCommandHandler(IAdminService adminService) : IRequestHandler<UpdateVehicleCommand, UpdateVehicleCommandResponse>
    {
        public async Task<UpdateVehicleCommandResponse> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            return await adminService.UpdateVehicle(request);
        }
    }
}
