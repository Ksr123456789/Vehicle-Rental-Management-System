using MediatR;
using RentalManagementSystem.Application.ServiceContracts;

namespace RentalManagementSystem.Application.Features.Auth.Logout
{
    public class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand, LogoutCommandResponse>
    {
        public async Task<LogoutCommandResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await authService.Logout(request);
        }
    }
}
