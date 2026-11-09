using MediatR;
using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateBookingStatus
{
    public class UpdateBookingStatusCommand : IRequest<UpdateBookingStatusCommandResponse>
    {
        public int BookingId { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}
