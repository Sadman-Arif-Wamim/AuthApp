using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;
using AuthProject.Identity;
using AuthProject.Data;
using System.Data;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly DBContext _context;

        public UserDetailsController(DBContext context)
        {
            _context = context;
        }

        [Authorize()]
        [RequiresClaim(IdentityData.AdminUserClaimName, "true")] 
        [HttpGet("getAllDetails/{id}")]      
        public JsonResult GetAllDetails(int id) 
        {
            User? user = _context.Users.FirstOrDefault(u => u.id == id);

            if (user != null) 
            {
                var response = new User
                {
                    userName = user.userName,
                    role = user.role,
                    id = id
                };
                return new JsonResult(Ok(response));
            }
            return new JsonResult(NotFound());
        }

        [Authorize()]
        [HttpGet("getRegularDetails/{id}")]
        public JsonResult GetRegularDetails(int id) 
        {
            User? user = new User();
            if (id != 1)
            {
                user = _context.Users.FirstOrDefault(u => u.id == id);
            }
            else
            {
                return new JsonResult(Unauthorized());
            }
 
            if (user != null)
            {
                var response = new User
                {
                    userName = user.userName,
                    role = user.role,
                    id = id
                };
                return new JsonResult(Ok(response));
            }
            return new JsonResult(NotFound());
        }
    }
}
