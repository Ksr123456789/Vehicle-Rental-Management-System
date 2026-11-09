using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetAllVehicleCategory
{
    public class GetAllVehicleCategoryQueryHandler(IAdminService adminService) : IRequestHandler<GetAllVehicleCategoryQuery, List<VehicleCategory>>
    {
        public async Task<List<VehicleCategory>> Handle(GetAllVehicleCategoryQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetAllVehicleCategory(request);
        }
    }
}
