using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AsyncInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsyncInn.Services
{
    public class IdentityUserService : IUserService
    {
        public readonly UserManager<ApplicationUser> userManager;
        private readonly JwtTokenService tokenService;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JwtTokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(30))
                };
            }

            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user =  await userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
            };
        }

        public async Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.Phonenumber,

            };
            var result = await userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(30))
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = 
                error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }
    }
}