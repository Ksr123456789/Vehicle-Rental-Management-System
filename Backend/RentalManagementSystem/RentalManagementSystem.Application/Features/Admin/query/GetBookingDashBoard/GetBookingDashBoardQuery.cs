using MediatR;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetBookingDashBoard
{
    public class GetBookingDashBoardQuery : IRequest<BookingDashBoardDto>
    {
       public int RentalCompanyId { get; set; }
    }
}
