using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).Matches("^[a-zA-Z][a-zA-Z\\s]+$");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(24).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]).+$");
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches("^[6-9]\\d{9}$");
            RuleFor(x => x.DrivingLicenseNumber).NotEmpty().Matches("^[a-zA-Z0-9]+$");
            RuleFor(x => x.LicenseExpiryDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty().MaximumLength(300);
        }
    }
}

