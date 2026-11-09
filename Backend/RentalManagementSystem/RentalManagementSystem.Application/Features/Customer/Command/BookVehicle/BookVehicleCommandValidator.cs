using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Customer.Command.BookVehicle
{
    public class BookVehicleCommandValidator : AbstractValidator<BookVehicleCommand>
    {
        public BookVehicleCommandValidator()
        {
            RuleFor(x => x.PickUpDateTime).NotEmpty();
            RuleFor(x => x.ReturnDateTime).NotEmpty();
            RuleFor(x => x.PickupLocation).NotEmpty();
            RuleFor(x => x.ReturnLocation).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();

        }
    }
}


/*
 
  public DateTime PickUpDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public int VehicleId { get; set; }
        public Boolean Insurance { get; set; }
 
 */