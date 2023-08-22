using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;
using AuthProject.Identity;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {

        [Authorize()]
        [RequiresClaim(IdentityData.AdminUserClaimName, "true")] 
        [HttpGet("getDetails")]      
        public JsonResult GetDetails() 
        {
            string userID = "Admin";
            string role = "Admin";

            var response = new UserDetails
            {
                userName = userID,
                role = role,
            };

            return new JsonResult(Ok(response));
        }
    }
}
