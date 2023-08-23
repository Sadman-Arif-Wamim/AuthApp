using AuthDirectory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AuthDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateTokenController : ControllerBase
    {
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

        private const string secretKey = "DefaultKeyGenerationNumber";
        private static readonly TimeSpan TokenLife = TimeSpan.FromHours(1);


        [HttpPost]
        [Route("token")]
        public IActionResult GenerateJwtToken(
            [FromBody] TokenResponse request)
        {
            
            TokenResponse tokenResponse = new TokenResponse();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, request.username),
                new (JwtRegisteredClaimNames.Email, request.username),
                new ("userid", request.role.ToString()),
            };

            foreach ( var claimPair in request.CustomClaims ) 
            {
                var jsonElement = (JsonElement)claimPair.Value;
                var valueType = jsonElement.ValueKind switch
                {
                    JsonValueKind.True => ClaimValueTypes.Boolean,
                    JsonValueKind.False => ClaimValueTypes.Boolean,
                    JsonValueKind.Number => ClaimValueTypes.Double,
                    _ => ClaimValueTypes.String,
                };

                var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);  
                claims.Add(claim);

            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLife),
                Issuer = "DefaultIssuer",
                Audience = "DefaultAudience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var tokenizer = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenizer);
            return Ok(token);
        }
    }
}
