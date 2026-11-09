using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle
{
    public class GetAvailablePagedVehicleQueryHandler(ICustomerService customerService) : IRequestHandler<GetAvailablePagedVehicleQuery, PagedResult<GetAvailablePagedVehicleQueryResponse>>
    {
        public async Task<PagedResult<GetAvailablePagedVehicleQueryResponse>> Handle(GetAvailablePagedVehicleQuery request, CancellationToken cancellationToken)
        {
            return await customerService.GetAvailablePagedVehicle(request);
        }
    }
}
