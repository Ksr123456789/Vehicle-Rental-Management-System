using MediatR;
using RentalManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle
{
    public class GetAvailablePagedVehicleQuery : IRequest<PagedResult<GetAvailablePagedVehicleQueryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? SearchBy { get; set; }
        public int? VehicleCategoryId { get; set; }
        public int? SeatingCapacity { get; set; }
        public int? RentalCompanyId { get; set; }
        public decimal? FromDailyRentalRate { get; set; }
        public decimal? ToDailyRentalRate { get; set; }

    }
}

