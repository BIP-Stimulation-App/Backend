using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.DAL.Context;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }

        #region User Own Data

        [HttpGet]
        [Authorize]
        public IActionResult GetUser()
        {
            try
            {
                return Ok(GetCurrentUser());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody]User user)
        {
            var result = _userService.UpdateUser(user);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPut("UpdateEmail")]
        public IActionResult UpdateEmail([FromQuery] string username, string email)
        {
            var result = _userService.UpdateEmail(username,email);
            return result is null ? NotFound() : Ok(result);
        }
        #endregion

        [HttpGet("UsernameInUse/{username}")]
        public IActionResult UsernameInUse([FromRoute] string username)
        {
            return _userService.UsernameInUse(username)?Ok("Username is not in use."): Ok("Username is already in use.");
        }

        private User? GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims;
            return _userService.GetUser(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

        }

    }
}
