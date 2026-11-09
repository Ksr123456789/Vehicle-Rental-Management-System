using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateRentalCompany
{
    public class UpdateRentalCompanyValidator : AbstractValidator<UpdateRentalCompanyCommand>
    {
        public UpdateRentalCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.HeadquartersLocation).NotEmpty().MaximumLength(200);
            RuleFor(x => x.LicenseNumber).NotEmpty();
        }
    }
}


