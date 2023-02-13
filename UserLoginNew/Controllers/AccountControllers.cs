using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using UserLoginNew;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace TokenApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller 
    {
        private readonly JWTSettings _options;
        public AccountController(IOptions<JWTSettings> optAccess)
        {
            _options = optAccess.Value;
        }

        [HttpPost("GetToken")]
        //https://localhost:7005/api/Account/GetToken
        public dynamic GetToken(string login, string pass, ApplicationContext db)
        {
            var users = db.Users.Where(Users => Users.Login == login & Users.Pass == pass).ToList();
                        
            foreach (Users u in users)
            {
                Console.WriteLine($"{u.Id} {u.Login} {u.Pass}");
            }
           
            if (users.Count == 1)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, login));
                claims.Add(new Claim("level", "123"));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
            return Unauthorized();
        }
    }
}
 