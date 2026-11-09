using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Application.Features.Auth.Logout
{
    public class LogoutCommand : IRequest<LogoutCommandResponse>
    {
    }
}
