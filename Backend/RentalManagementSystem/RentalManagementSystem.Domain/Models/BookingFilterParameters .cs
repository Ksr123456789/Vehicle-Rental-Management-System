using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class BookingFilterParameters : PaginationParameters
    {
        public BookingStatus? BookingStatus { get; set; }

        public int? RentalCompanyId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
