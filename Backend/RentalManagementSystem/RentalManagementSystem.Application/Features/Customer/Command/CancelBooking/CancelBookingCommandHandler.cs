using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Command.CancelBooking
{
    public class CancelBookingCommandHandler(ICustomerService customerService) : IRequestHandler<CancelBookingCommand, CancelBookingCommandResponse>
    {
        public async Task<CancelBookingCommandResponse> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            return await customerService.CancelBooking(request);
        }
    }
}
