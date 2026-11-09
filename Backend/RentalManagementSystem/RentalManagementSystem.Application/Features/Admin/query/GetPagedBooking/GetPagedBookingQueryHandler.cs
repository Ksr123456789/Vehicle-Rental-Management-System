using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedBooking
{
    public class GetPagedBookingQueryHandler(IAdminService adminService) : IRequestHandler<GetPagedBookingQuery, PagedResult<GetPagedBookingQueryResponse>>
    {
        public async Task<PagedResult<GetPagedBookingQueryResponse>> Handle(GetPagedBookingQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetPagedBooking(request);
        }
    }
}
