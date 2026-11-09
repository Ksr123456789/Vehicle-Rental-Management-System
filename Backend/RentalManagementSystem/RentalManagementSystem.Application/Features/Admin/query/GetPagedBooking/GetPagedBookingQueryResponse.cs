using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedBooking
{
    public class GetPagedBookingQueryResponse
    {

        public int Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public int TotalDays { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookedOn { get; set; }
        public string CompanyName { get; set; }
        public string VehicleModel { get; set; }

    }
}
