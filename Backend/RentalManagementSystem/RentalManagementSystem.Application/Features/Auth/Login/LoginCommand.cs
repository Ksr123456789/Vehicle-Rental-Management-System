using MediatR;

namespace RentalManagementSystem.Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
