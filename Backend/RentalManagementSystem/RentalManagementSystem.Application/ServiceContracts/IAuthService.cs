using RentalManagementSystem.Application.Features.Auth.Login;
using RentalManagementSystem.Application.Features.Auth.Logout;
using RentalManagementSystem.Application.Features.Auth.Refresh;
using RentalManagementSystem.Application.Features.Auth.Register;

namespace RentalManagementSystem.Application.ServiceContracts
{
    public interface IAuthService
    {

        Task<RegisterCommandResponse> Register(RegisterCommand command);
        Task<LoginCommandResponse> Login(LoginCommand command);
        Task<RefreshCommandResponse> Refresh(RefreshCommand command);
        Task<LogoutCommandResponse> Logout(LogoutCommand command);

    }
}
