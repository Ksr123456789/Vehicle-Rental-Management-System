using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class AvailableVehicleFilterParameters : PaginationParameters
    {
        public int? VehicleCategoryId { get; set; }

        public int? SeatingCapacity { get; set; }

        public int? RentalCompanyId { get; set; }

        public decimal? FromDailyRentalRate { get; set; }

        public decimal? ToDailyRentalRate { get; set; }
    }
}
