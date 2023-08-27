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

            User? userdetails = _context.Users.FirstOrDefault(u => u.userName == user.userName && u.password == user.password);

            if(userdetails != null)
            {
                isAuthenticated = true;

                var response = new User
                {
                    userName = userdetails.userName,
                    password = userdetails.password,
                    role = userdetails.role,
                    id = userdetails.id
                };

                return new JsonResult(Ok(response));
            }

            return new JsonResult(Unauthorized());
        }       
    }
}
