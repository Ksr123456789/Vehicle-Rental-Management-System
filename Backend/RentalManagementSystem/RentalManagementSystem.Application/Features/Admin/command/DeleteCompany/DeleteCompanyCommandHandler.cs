using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.DeleteCompany
{
    public class DeleteCompanyCommandHandle(IAdminService adminService) : IRequestHandler<DeleteCompanyCommand, DeleteCompanyCommandResponse>
    {
        public async Task<DeleteCompanyCommandResponse> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            return await adminService.DeleteRentalCompany(request);
        }
    }
}
