using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Domain.Entities
{
    public class Vehicle
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
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public RentalCompany RentalCompany { get; set; }
        public int RentalCompanyId { get; set; }
        public VehicleCategory VehicleCategory { get; set; }
        public int VehicleCategoryId { get; set; }
        public ICollection<Booking> Bookings { get; set; }  = new List<Booking>();
    }
}


