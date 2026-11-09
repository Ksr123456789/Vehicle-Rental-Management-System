using MediatR;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedVehicle
{
    public class GetPagedVehicleQuery : IRequest<PagedResult<GetPagedVehicleQueryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? SearchBy { get; set; }
        public int? VehicleCategoryId { get; set; }
        public VehicleAvailabilityStatus? VehicleAvailabilityStatus { get; set; }



    }
}
