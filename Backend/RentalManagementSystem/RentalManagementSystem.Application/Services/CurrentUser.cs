using Microsoft.AspNetCore.Http;
using RentalManagementSystem.Application.ServiceContracts;
using System.Security.Claims;

namespace RentalManagementSystem.Application.Services
{
    public class CurrentUser : ICurrentUser
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Email => _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
    
    }
}
