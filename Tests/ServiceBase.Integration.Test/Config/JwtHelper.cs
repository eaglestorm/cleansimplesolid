using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ServiceBase.Integration.Test.Config
{
    public static class JwtHelper
    {
        public static string GetHeaderValue(string secret)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub,"username")
                }),
                Expires = DateTime.Now.AddDays(1),
                Issuer = "eldar systems",
                Audience = "service base",
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(secret),SecurityAlgorithms.HmacSha256 )
            };
            
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
        
        private static SecurityKey GetSymmetricSecurityKey(string secret)
        {
            byte[] symmetricKey = Convert.FromBase64String(secret);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}