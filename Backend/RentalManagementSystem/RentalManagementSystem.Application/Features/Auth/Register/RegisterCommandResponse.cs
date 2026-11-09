using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Register
{
    public class RegisterCommandResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
