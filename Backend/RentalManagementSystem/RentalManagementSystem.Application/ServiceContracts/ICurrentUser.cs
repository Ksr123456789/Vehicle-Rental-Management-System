using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.ServiceContracts
{
    public interface ICurrentUser
    {
        public string? UserId { get; }
        public string? Email { get; }
    }
}
