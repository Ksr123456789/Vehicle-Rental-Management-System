using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class VehicleFilterParameters : PaginationParameters
    {
        public int? VehicleCategoryId { get; set; }

        public VehicleAvailabilityStatus? VehicleAvailabilityStatus { get; set; }
    }
}
