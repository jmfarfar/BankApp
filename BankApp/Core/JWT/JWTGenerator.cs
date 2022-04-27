using BankApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp.Core.JWT
{
    public class JWTGenerator : IJWTGenerator
    {
        public string CreateToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("role", user.Role.ToString()),
                new Claim("usdbalance", user.USDBalance.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gcpSJNMRhkT5zRY6SUcNp04V9Ly1EF8B"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
