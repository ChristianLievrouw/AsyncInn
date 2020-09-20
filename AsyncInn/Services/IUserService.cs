using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsyncInn.Services
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState);

        Task<UserDto> Authenticate(string username, string password);

        Task<UserDto> GetUser(ClaimsPrincipal user);
    }
}
