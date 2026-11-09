using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateBookingStatus
{
    public class UpdateBookingStatusCommandHandler(IAdminService adminService) : IRequestHandler<UpdateBookingStatusCommand, UpdateBookingStatusCommandResponse>
    {
        public async Task<UpdateBookingStatusCommandResponse> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
        {
            return await adminService.UpdateBookingStatus(request);
        }
    }
}
