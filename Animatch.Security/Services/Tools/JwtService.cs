using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Animatch.Security.Services.Tools
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateToken(User user, Guid? shelterId = null)
        {

            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "30"));

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("accountType", user.AccountType.ToString())
    };

            // 🌟 PLUS DE CS1061 ! On utilise directement le paramètre shelterId passé par le service d'auth
            if (user.AccountType.ToString() == "Shelter" && shelterId.HasValue)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, shelterId.Value.ToString()));
            }
            else
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
