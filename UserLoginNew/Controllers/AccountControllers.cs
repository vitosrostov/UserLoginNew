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
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace TokenApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IDateTime _datatime;
        private readonly JWTSettings _options;
        private readonly ApplicationContext _contextDB;
        private readonly IGetToken _tokenService;
        public AccountController(IOptions<JWTSettings> optAccess, ApplicationContext context, IDateTime datetime, IGetToken tokenService)
        {
            _datatime = datetime;
            _options = optAccess.Value;
            _contextDB = context;
            _tokenService = tokenService;
        }

        [HttpPost("token")]
        //https://localhost:7005/api/Account/token
        public IActionResult GetToken([FromBody] UserCredentialsModel prms)
        {
            var users = _contextDB.Users.Where(Users => Users.Login == prms.Login).ToList();

            if (users.Count == 1)
            {
                string passHash = BCrypt.Net.BCrypt.HashPassword(prms.Password);
                bool passVerify = BCrypt.Net.BCrypt.Verify(prms.Password, users.ElementAt(0).Pass); // enter pass and dbpass verify
                if (passVerify)
                {
                    return Ok(new
                    {
                        Success = "True",
                        Token = _tokenService.TokenGet(prms.Login, users.ElementAt(0).Role),
                        login = prms.Login,
                        password = passHash,
                        Role = users.ElementAt(0).Role
                    });
                }
            }
            return Unauthorized(new 
            { 
                Success = "False", 
                error = "Please pass the valid login and password" 
            });
        }
                   
        [HttpPost("registration")]
        public IActionResult CreateUser([FromBody] UserCredentialsModel prms)
        {
            var find_free_login = _contextDB.Users.Where(Users => Users.Login == prms.Login).ToList();
            if (find_free_login.Count == 0)
            {
                string passHashNewUser = BCrypt.Net.BCrypt.HashPassword(prms.Password);
                var user = _contextDB.Users.Add(new Users() { Login = prms.Login, Pass = passHashNewUser, Role = UserRoles.User });
                _contextDB.SaveChanges(); // save new user to db, pass crypted
                return Ok(new
                {
                    Success = "True",
                    login = prms.Login,
                    password = passHashNewUser,
                    Role = UserRoles.User
                });
            }
            return BadRequest(new
            { 
                Success = "False", 
                error = "User already exists"
            });
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("all-users")]
        public IActionResult GetallUsers() //return all users from db only admin role autorized 
        {
            var all_users_db = _contextDB.Users.ToList();
            return Ok(all_users_db);
        }
    }
}
 