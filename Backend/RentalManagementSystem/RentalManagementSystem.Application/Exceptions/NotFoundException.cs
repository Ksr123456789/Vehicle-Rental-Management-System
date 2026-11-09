using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RentalManagementSystem.Application.Exceptions
{
    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
