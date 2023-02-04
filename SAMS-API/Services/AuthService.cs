using API.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApp.Models;

namespace API.Services
{
    public class AuthService
    {
        private List<Claim> GenerateClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            return claims;
        }

        private string GenerateRefresh()
        {
            var random = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public TokenResponse GenerateToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("samsSecurePassword1234"));
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(30),
                claims: GenerateClaims(user),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Refresh = GenerateRefresh(),
                ID = user.ID.ToString(),
                User = user
            };
        }
    }
}
