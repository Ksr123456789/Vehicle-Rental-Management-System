using MediatR;

namespace RentalManagementSystem.Application.Features.Admin.command.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<DeleteCompanyCommandResponse>
    {
        public int CompanyId { get; set; }
    }
}
