using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalManagementSystem.Application.Features.Auth.Login;
using RentalManagementSystem.Application.Features.Auth.Logout;
using RentalManagementSystem.Application.Features.Auth.Refresh;
using RentalManagementSystem.Application.Features.Auth.Register;

namespace RentalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout( LogoutCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

    }
}
