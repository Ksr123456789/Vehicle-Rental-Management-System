using MediatR;
using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Application.Features.Admin.command.UpdateRentalCompany
{
    public class UpdateRentalCompanyCommand : IRequest<UpdateRentalCompanyCommandResponse>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public CompanyType CompanyType { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string HeadquartersLocation { get; set; }
        public string LicenseNumber { get; set; }
        public CompanyStatus Status { get; set; }
    }
}
