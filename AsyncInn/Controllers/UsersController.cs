using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Models;
using AsyncInn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterData data)
        {
            UserDto user = await userService.Register(data, this.ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));

            }
            return user;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData data)
        {
            var user = await userService.Authenticate(data.Username, data.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        }

        [HttpGet("Self")]
        public async Task<ActionResult<UserDto>> Self()
        {
            return await userService.GetUser(this.User);
        }
    }
}