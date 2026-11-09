using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Entities
{
    public class ExtraService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<BookingExtraService> BookingExtraServices { get; set; } = new List<BookingExtraService>();



    }
}
