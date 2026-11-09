using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public Boolean Insurance { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public int TotalDays { get; set; }
        public decimal RentalAmount { get; set; }
        public decimal AdditionalServiceAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public DateTime BookedOn { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public ApplicationUser Customer { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<BookingExtraService> BookingExtraServices = new List<BookingExtraService>();
    }
}

