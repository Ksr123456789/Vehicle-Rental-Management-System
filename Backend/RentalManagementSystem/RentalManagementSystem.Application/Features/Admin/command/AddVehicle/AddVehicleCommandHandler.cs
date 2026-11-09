using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddVehicle
{
    public class AddVehicleCommandHandler(IAdminService adminService) : IRequestHandler<AddVehicleCommand, AddVehicleCommandResponse>
    {
        public async Task<AddVehicleCommandResponse> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            return await adminService.AddVehicle(request);
        }
    }
}
