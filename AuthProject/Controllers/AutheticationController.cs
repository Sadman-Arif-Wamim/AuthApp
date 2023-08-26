    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;
using AuthProject.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System;
using Microsoft.Extensions.Configuration;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly DBContext _context;

        public AutheticationController(DBContext context) 
        {
            _context = context; 
        }

        [HttpPost("authenticate")]
        public JsonResult AutheticateUser(User user)
        {

            bool isAuthenticated = false;

            if (user == null || user.userName == null || user.password == null)
            {
                return new JsonResult(NotFound());
            }

            else if(user.userName == "admin@user.com" && user.password == "password1234")
            {
                isAuthenticated = true;
            }
            
            if (!isAuthenticated)
            {
                return new JsonResult(Unauthorized());
            }

            var response = new UserResponse
            {
                userName = user.userName,
                password = user.password,          
                id = 1
            };

            return new JsonResult(Ok(response));
        }       
    }
}
