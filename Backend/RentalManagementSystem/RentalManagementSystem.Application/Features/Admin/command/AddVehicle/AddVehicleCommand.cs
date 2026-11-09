using MediatR;
using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.command.AddVehicle
{
    public class AddVehicleCommand : IRequest<AddVehicleCommandResponse>
    {
        public string MakeAndModel { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearofManufacture { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal DailyRentalRate { get; set; }
        public VehicleAvailabilityStatus VehicleAvailabilityStatus { get; set; }
        public int Mileage { get; set; }
        public int RentalCompanyId { get; set; }
        public int VehicleCategoryId { get; set; }
    }
}


