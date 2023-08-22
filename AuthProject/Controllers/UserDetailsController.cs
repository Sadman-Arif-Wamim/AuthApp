using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        [HttpGet("getDetails")]
        [Authorize]
        public JsonResult GetDetails(int id) 
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
