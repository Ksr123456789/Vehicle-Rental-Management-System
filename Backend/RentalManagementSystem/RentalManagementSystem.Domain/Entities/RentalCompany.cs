using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Domain.Entities
{
    public class RentalCompany
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public CompanyType CompanyType { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string HeadquartersLocation { get; set; }
        public string LicenseNumber { get; set; }
        public CompanyStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}

