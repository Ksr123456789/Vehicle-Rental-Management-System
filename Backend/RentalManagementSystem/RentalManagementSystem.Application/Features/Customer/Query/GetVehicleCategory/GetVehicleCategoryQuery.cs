using MediatR;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetVehicleCategory
{
    public class GetVehicleCategoryQuery : IRequest<List<VehicleCategory>>
    {
    }
}
