using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetRentalCompany
{
    public class GetRentalCompanyQueryHandler(ICustomerService customerService) : IRequestHandler<GetRentalCompanyQuery, List<RentalCompany>>
    {
        public async Task<List<RentalCompany>> Handle(GetRentalCompanyQuery request, CancellationToken cancellationToken)
        {
            return await customerService.GetRentalCompany(request);
        }
    }
}
