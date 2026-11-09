using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateBookingStatus
{
    public class UpdatBookingStatusCommandValidator : AbstractValidator<UpdateBookingStatusCommand>
    {
        public UpdatBookingStatusCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x => x.BookingStatus).IsInEnum();
        }
    }
}
