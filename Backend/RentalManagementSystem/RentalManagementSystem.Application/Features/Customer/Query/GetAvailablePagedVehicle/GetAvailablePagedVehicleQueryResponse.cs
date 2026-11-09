using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle
{
    public class GetAvailablePagedVehicleQueryResponse
    {

        public int Id { get; set; }
        public string MakeAndModel { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearofManufacture { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal DailyRentalRate { get; set; }
        public int Mileage { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CategoryName { get; set; }

    }
}
