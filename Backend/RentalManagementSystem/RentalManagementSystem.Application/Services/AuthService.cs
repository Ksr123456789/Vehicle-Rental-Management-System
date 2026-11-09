using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using RentalManagementSystem.Application.Exceptions;
using RentalManagementSystem.Application.Features.Auth.Login;
using RentalManagementSystem.Application.Features.Auth.Logout;
using RentalManagementSystem.Application.Features.Auth.Refresh;
using RentalManagementSystem.Application.Features.Auth.Register;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Constants;
using RentalManagementSystem.Domain.Entities;
using System.Security.Claims;

namespace RentalManagementSystem.Application.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ITokenService tokenService, ICurrentUser currentUser ) : IAuthService
    {

        public async Task<RegisterCommandResponse> Register(RegisterCommand command)
        {
            var existingUser = await userManager.FindByEmailAsync(command.Email);
            if (existingUser != null)
            {
                throw new BadRequestException("User with this email already exists");
            }

            if (command.Password != command.ConfirmPassword)
            {
                throw new ConflictException("password and confirm password doesn't match");
            }

            var existByDrivingLicense =  userManager.Users.FirstOrDefault(x => x.DrivingLicenseNumber == command.DrivingLicenseNumber);

            if (existByDrivingLicense != null)
            {
                throw new ConflictException("user with given driving license number already exists");
            }



            


            ApplicationUser user = new()
            {
                Email = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = command.Email,
                FullName = command.FullName,
                PhoneNumber = command.PhoneNumber,
                DrivingLicenseNumber = command.DrivingLicenseNumber,
                LicenseExpiryDate = command.LicenseExpiryDate,
                Address = command.Address,
                EmailConfirmed = true,
            };

            var createUserResult = await userManager.CreateAsync(user, command.Password);


            if (createUserResult.Succeeded == false)
            {
                var errors = createUserResult.Errors.Select(e => e.Description);

                throw new BadRequestException($"Failed to create user. Errors: {string.Join(", ", errors)}");
            }

            var addUserToRoleResult = await userManager.AddToRoleAsync(user: user, role: Roles.Customer);

            if (addUserToRoleResult.Succeeded == false)
            {
                var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                throw new BadRequestException($"Failed to assign user role. Errors: {string.Join(", ", errors)}");
            }




            List<Claim> authClaims = [
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ];

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var accessToken = tokenService.GenerateAccessToken(authClaims);

            string refreshToken = tokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);





            return new RegisterCommandResponse
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }




        public async Task<LoginCommandResponse> Login(LoginCommand command)
        {

            var user = await userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                throw new BadRequestException("User with this Email is not registered with us.");
            }
            bool isValidPassword = await userManager.CheckPasswordAsync(user, command.Password);
            if (isValidPassword == false)
            {
                throw new BadRequestException("invalid credentials");
            }

            List<Claim> authClaims = [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("driving_license", user.DrivingLicenseNumber)
            ];

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var accessToken = tokenService.GenerateAccessToken(authClaims);

            string refreshToken = tokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);

            return new LoginCommandResponse
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }

        public async Task<LogoutCommandResponse> Logout(LogoutCommand command)
        {
            if (currentUser.UserId == null)
            {
                throw new Exception("unauthorized access");
            }

            var user = await userManager.FindByIdAsync(currentUser.UserId);

            if (user == null)
            {
                throw new Exception("unAuthorized access");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await userManager.UpdateAsync(user);

            return new LogoutCommandResponse { };
        }

        public async Task<RefreshCommandResponse> Refresh(RefreshCommand command)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(command.accessToken);
            var username = principal.Identity.Name;

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("user is null.");
            }

            if (user == null || user.RefreshToken != command.refreshToken)
            {
                 throw new Exception("refresh token doesn't match.");
            }

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("refresh token time expired.");
            }

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);

            return new RefreshCommandResponse
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            };
        }

        
    
    }
    
}
