using MediatR;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetAllExtraServices
{
    public class GetAllExtraServicesQuery : IRequest<List<ExtraService>>
    {
    }
}
