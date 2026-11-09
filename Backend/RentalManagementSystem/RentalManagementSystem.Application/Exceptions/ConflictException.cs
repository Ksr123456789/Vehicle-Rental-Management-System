using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RentalManagementSystem.Application.Exceptions
{
    public sealed class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
