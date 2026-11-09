using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals
{
    public class MyPagedRentalsQueryResponse
    {
        public int Id { get; set; }
        public string MakeModal { get; set; }
        public int SeatingCapacity { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDays { get; set; }
        public decimal AdditionalServiceAmount { get; set; }
        public decimal RentalAmount { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationNumber { get; set; }
        public int YearofManufacture { get; set; }
        public decimal DailyRentalRate { get; set; }
        public int Mileage { get; set; }
    }
}


