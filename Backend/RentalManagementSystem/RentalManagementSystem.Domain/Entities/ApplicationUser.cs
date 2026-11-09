using Microsoft.AspNetCore.Identity;

namespace RentalManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string Address { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}


