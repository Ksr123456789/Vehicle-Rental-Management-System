using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Login
{
    public class LoginCommandResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
