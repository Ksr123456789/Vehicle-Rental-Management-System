using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedVehicle
{
    public class GetPagedVehicleQueryResponse
    {

        public int Id { get; set; }
        public string MakeAndModel { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearofManufacture { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal DailyRentalRate { get; set; }
        public VehicleAvailabilityStatus VehicleAvailabilityStatus { get; set; }
        public int Mileage { get; set; }
        public string VehicleCategory { get; set; }
        public int RentalCompanyId { get; set; }
        public string RentalCompanyName { get; set; }
        public int VehicleCategoryId { get; set; }

    }
}
