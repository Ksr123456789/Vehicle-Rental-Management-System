using MediatR;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetAllVehicleCategory
{
    public class GetAllVehicleCategoryQuery : IRequest<List<VehicleCategory>>
    {
    }
}
