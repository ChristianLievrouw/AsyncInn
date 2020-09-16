using System;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Register(RegisterData data);
    }
}
