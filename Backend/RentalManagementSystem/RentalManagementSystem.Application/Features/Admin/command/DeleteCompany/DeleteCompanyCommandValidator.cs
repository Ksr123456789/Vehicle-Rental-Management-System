using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.DeleteCompany
{
    public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}
