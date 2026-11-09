using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddRentalCompany
{
    public class AddRentalCompanyCommandHandler(IAdminService adminService) : IRequestHandler<AddRentalCompanyCommand, AddRentalCompanyCommandResponse>
    {
        public async Task<AddRentalCompanyCommandResponse> Handle(AddRentalCompanyCommand request, CancellationToken cancellationToken)
        {
            return await adminService.AddRentalCompany(request);
        }
    }
}
