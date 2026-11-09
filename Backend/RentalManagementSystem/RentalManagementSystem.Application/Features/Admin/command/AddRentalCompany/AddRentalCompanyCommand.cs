using MediatR;
using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddRentalCompany
{
    public class AddRentalCompanyCommand : IRequest<AddRentalCompanyCommandResponse>
    {
        public string CompanyName { get; set; }
        public CompanyType CompanyType { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string HeadquartersLocation { get; set; }
        public string LicenseNumber { get; set; }
        public CompanyStatus Status { get; set; }
    }
}

