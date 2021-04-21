using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        // Type of encryption where only one key is used to encrypt and decrypt
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            // Adding claims which is a part of security
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };
            // Creating credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            // This is to describer how the token should look like
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            // This is token handler to handle the token
            var tokenHandler = new JwtSecurityTokenHandler();
            // Then we finally create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Return the created token
            return tokenHandler.WriteToken(token);
        }
    }
}