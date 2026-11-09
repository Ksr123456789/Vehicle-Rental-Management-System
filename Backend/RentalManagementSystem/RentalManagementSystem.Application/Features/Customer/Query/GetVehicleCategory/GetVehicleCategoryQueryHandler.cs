using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetVehicleCategory
{
    public class GetVehicleCategoryQueryHandler(ICustomerService customerService) : IRequestHandler<GetVehicleCategoryQuery, List<VehicleCategory>>
    {
        public async Task<List<VehicleCategory>> Handle(GetVehicleCategoryQuery request, CancellationToken cancellationToken)
        {
            return await customerService.GetVehicleCategory(request);
        }
    }
}
