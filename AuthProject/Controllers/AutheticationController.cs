using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;
using AuthProject.Data;
using System.IdentityModel.Tokens.Jwt;

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
            if (user == null || user.userName == null || user.password == null)
            {
                return new JsonResult(NotFound());
            }

            bool isAuthenticated = true;

            if (!isAuthenticated)
            {
                return new JsonResult(Unauthorized());
            }

            var token = "";
            var response = new UserResponse
            {
                userName = user.userName,
                password = user.password,
                token = token
            };

            return new JsonResult(Ok(response));
        }
    }
}
