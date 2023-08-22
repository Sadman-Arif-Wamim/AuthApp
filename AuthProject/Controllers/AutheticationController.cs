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

            var token = GenerateJwtToken(user.userName);
            var response = new UserResponse
            {
                userName = user.userName,
                password = user.password,
                token = token,
                id = 1
            };

            return new JsonResult(Ok(response));
        }
        
        private static string GenerateSecretKey() 
        {
            int keyLength = 32;

            byte[] secretKey = GenerateRandomKey(keyLength);

            string base64Key = Convert.ToBase64String(secretKey);

            return base64Key;
        }

        private static byte[] GenerateRandomKey(int keyLength)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[keyLength];
                rng.GetBytes(key);
                return key;
            }
        }

        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DefaultKeyGenerationNumber");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = "DefaultIssuer",
                Audience = "DefaultAudience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
