using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddRentalCompany
{
    public class AddRentalCompanyCommandValidator : AbstractValidator<AddRentalCompanyCommand>
    {
        public AddRentalCompanyCommandValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches("^[6-9]\\d{9}$");
            RuleFor(x => x.HeadquartersLocation).NotEmpty().MaximumLength(200);
            RuleFor(x => x.LicenseNumber).NotEmpty();
        }
    }
}


