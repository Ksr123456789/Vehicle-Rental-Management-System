using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Command.CancelBooking
{
    public class CancelBookingCommand : IRequest<CancelBookingCommandResponse>
    {

        public int BookingId { get; set; }

    }
}
