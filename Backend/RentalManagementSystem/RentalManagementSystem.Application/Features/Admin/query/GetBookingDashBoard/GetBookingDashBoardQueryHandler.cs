using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetBookingDashBoard
{
    public class GetBookingDashBoardQueryHandler(IAdminService adminService) : IRequestHandler<GetBookingDashBoardQuery, BookingDashBoardDto>
    {
        public async Task<BookingDashBoardDto> Handle(GetBookingDashBoardQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetBookingDashBoardDetails(request);
        }
    }
}