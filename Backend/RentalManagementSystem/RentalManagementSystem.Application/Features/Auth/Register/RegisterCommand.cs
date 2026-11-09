using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResponse>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string Address { get; set; }




    }
}

