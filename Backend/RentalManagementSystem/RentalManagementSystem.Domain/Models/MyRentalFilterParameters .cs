using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class MyRentalFilterParameters : PaginationParameters
    {
        public BookingStatus? BookingStatus { get; set; }

        public Guid CustomerId { get; set; }

        public int? SeatingCapacity { get; set; }
    }
}
