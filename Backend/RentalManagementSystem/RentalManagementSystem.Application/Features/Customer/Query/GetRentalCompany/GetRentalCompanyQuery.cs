using MediatR;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetRentalCompany
{
    public class GetRentalCompanyQuery : IRequest<List<RentalCompany>>
    {
    }
}
