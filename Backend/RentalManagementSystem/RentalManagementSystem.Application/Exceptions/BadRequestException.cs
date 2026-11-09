using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RentalManagementSystem.Application.Exceptions
{
    public sealed class BadRequestException : AppException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
