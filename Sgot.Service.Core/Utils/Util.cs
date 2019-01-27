using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sgot.Service.Core.Utils
{
    public class Util
    {
        public static string CreateToken(ApplicationUser user, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var claims = userManager.GetClaimsAsync(user).Result;
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddSeconds(int.Parse(configuration.GetValue<string>("TokenConfigurations:Seconds")))).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            //gera token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<String>("TokenConfigurations:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwrHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(claims);
            var jwt = new JwtSecurityToken(jwrHeader, jwtPayload);
            return "bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
