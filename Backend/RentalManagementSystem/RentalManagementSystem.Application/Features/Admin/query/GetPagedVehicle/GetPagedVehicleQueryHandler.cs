using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedVehicle
{
    internal class GetPagedVehicleQueryHandler(IAdminService adminService) : IRequestHandler<GetPagedVehicleQuery, PagedResult<GetPagedVehicleQueryResponse>>
    {
        public async Task<PagedResult<GetPagedVehicleQueryResponse>> Handle(GetPagedVehicleQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetPagedVehicle(request);
        }
    }
}
