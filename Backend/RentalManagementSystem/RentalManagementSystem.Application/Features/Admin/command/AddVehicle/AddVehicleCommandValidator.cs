using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        public AddVehicleCommandValidator()
        {
            RuleFor(x => x.MakeAndModel).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(20);
            RuleFor(x => x.YearofManufacture).NotEmpty();
            RuleFor(x => x.SeatingCapacity).NotEmpty().GreaterThan(0).LessThan(21);
            RuleFor(x => x.DailyRentalRate).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Mileage).NotEmpty().GreaterThan(0);

        }
    }
}


