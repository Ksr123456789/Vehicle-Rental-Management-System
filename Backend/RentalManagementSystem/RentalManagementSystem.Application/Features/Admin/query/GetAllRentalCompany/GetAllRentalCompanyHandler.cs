using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetAllRentalCompany
{
    public class GetAllRentalCompanyHandler(IAdminService adminService) : IRequestHandler<GetAllRentalCompanyQuery, List<RentalCompany>>
    {
        public async Task<List<RentalCompany>> Handle(GetAllRentalCompanyQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetAllRentalCompany(request);
        }
    }
}
