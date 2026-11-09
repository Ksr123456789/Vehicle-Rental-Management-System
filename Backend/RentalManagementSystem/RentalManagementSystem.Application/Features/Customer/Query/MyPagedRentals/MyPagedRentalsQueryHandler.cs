using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals
{
    public class MyPagedRentalsQueryHandler(ICustomerService customerService) : IRequestHandler<MyPagedRentalsQuery, PagedResult<MyPagedRentalsQueryResponse>>
    {
        public async Task<PagedResult<MyPagedRentalsQueryResponse>> Handle(MyPagedRentalsQuery request, CancellationToken cancellationToken)
        {
            return await customerService.MyPagedRentals(request);
        }
    }
}
