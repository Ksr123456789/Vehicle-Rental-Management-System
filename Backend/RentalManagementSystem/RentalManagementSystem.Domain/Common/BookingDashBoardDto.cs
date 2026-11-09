using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Common
{
    public class BookingDashBoardDto
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal FleetUtilizationRate { get; set; }
    }
}
