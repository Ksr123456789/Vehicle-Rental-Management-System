using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.MakeAndModel).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(20);
            RuleFor(x => x.RegistrationNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SeatingCapacity).NotEmpty().GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(x => x.DailyRentalRate).NotEmpty().GreaterThan(0);
            RuleFor(x => x.RentalCompanyId).NotEmpty();
            RuleFor(x => x.VehicleCategoryId).NotEmpty();
        }
    }
}


