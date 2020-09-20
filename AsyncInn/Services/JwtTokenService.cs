using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AsyncInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AsyncInn.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public JwtTokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        public async Task<string> GetToken(ApplicationUser user, TimeSpan expireIn)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            if (principal == null) return null;
            var signinkey = GetSecurityKey(configuration);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expireIn,
                signingCredentials: new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null) throw new InvalidOperationException("JWT:Secret is missing");

            var securityBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(securityBytes);
        }
    }
}
