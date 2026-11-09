using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Refresh
{
    public class RefreshCommandResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
