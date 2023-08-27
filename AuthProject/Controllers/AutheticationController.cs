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
using AuthDirectory.Models;
using System.Net.Http;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly HttpClient _httpClient;

        public AutheticationController(DBContext context, HttpClient httpClient) 
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AutheticateUser(User user)
        {

            if (user == null || user.userName == null || user.password == null)
            {
                return new JsonResult(NotFound());
            }

            User? userdetails = _context.Users.FirstOrDefault(u => u.userName == user.userName && u.password == user.password);

            if(userdetails != null)
            {            
                var response = new User
                {
                    userName = userdetails.userName,
                    password = userdetails.password,
                    role = userdetails.role,
                    id = userdetails.id
                };

                var token = await GetToken(response);

                if (!string.IsNullOrEmpty(token))
                {
                    response.token = token;
                    return Ok(response);
                }
            }

            return new JsonResult(Unauthorized());
        }


        private async Task<string> GetToken(User user)
        {
            var tokenResponse = new TokenResponse
            {
                username = user.userName,
                role = user.role,
                IncludeAdminClaim = user.role == "admin" ? true : false 
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7203/api/GenerateToken/token", tokenResponse);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                return "";
            }
        }
    }
}
