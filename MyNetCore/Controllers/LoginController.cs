using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class LoginController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public Result Authenticate()
        {
            Result ret = new Result();
            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(JwtClaimTypes.Audience,"api"),
                new Claim(JwtClaimTypes.Issuer,"MyNetCore"),
                new Claim(JwtClaimTypes.Id, "1"),
                new Claim(JwtClaimTypes.Name, "BobChen"),
                new Claim(JwtClaimTypes.IdentityProvider, "123456")
              }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Startup.symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var jwt = new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    //sid = "1",
                    //name = "BobChen",
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            };
            ret.Data = jwt;
            return ret;
        }


       
    }
}
