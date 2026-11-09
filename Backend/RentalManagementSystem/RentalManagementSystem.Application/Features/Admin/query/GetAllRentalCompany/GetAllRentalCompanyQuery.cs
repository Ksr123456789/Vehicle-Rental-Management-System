using MediatR;
using RentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetAllRentalCompany
{
    public class GetAllRentalCompanyQuery : IRequest<List<RentalCompany>>
    {
    }
}
