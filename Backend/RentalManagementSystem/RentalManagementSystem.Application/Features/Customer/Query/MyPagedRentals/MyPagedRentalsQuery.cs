using MediatR;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals
{
    public class MyPagedRentalsQuery : IRequest<PagedResult<MyPagedRentalsQueryResponse>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? SearchBy { get; set; }
        public BookingStatus? BookingStatus { get; set; }
        public int? SeatingCapacity { get; set; }

    }
}
