using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Entities
{
    public class BookingExtraService
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public ExtraService ExtraService { get; set; }
        public int ExtraServiceId { get; set; }
    }
}
