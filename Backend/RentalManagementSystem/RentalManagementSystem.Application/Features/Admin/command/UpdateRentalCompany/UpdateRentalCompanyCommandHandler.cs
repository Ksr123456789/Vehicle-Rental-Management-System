using MediatR;
using RentalManagementSystem.Application.ServiceContracts;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateRentalCompany
{
    public class UpdateRentalCompanyCommandHandler(IAdminService adminService) : IRequestHandler<UpdateRentalCompanyCommand, UpdateRentalCompanyCommandResponse>
    {
        public async Task<UpdateRentalCompanyCommandResponse> Handle(UpdateRentalCompanyCommand request, CancellationToken cancellationToken)
        {
            return await adminService.UpdateRentalCompany(request);
        }
    }
}
