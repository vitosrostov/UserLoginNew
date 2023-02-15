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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TokenApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller 
    {
        private readonly IDateTime _datatime;
        private readonly JWTSettings _options;
        private readonly ApplicationContext _context;
        private readonly IGetToken _token;
        public AccountController(IOptions<JWTSettings> optAccess, ApplicationContext context, IDateTime datetime, IGetToken token)
        {
            _datatime = datetime;
            _options = optAccess.Value;
            _context = context;
            _token = token;
        }

        [HttpPost("GetToken")]
        //https://localhost:7005/api/Account/GetToken
        public string GetToken(string login, string pass)
        {
            var users = _context.Users.Where(Users => Users.Login == login & Users.Pass == pass).ToList();
                        
            foreach (Users u in users)
            {
                Console.WriteLine($"{u.Id} {u.Login} {u.Pass}");
            }
           
            if (users.Count == 1)
            {

                return _token.TokenGet(login);
            }
            return null;
        }
    }
}
 