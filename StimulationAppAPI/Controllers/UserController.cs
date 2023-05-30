using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.DAL.Context;
using StimulationAppAPI.DAL.Model.Requests;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }

        #region User Own Data

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                return Ok(new UserResponds(GetCurrentUser()!));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserResponds), 200), ProducesResponseType(404)]
        public IActionResult UpdateUser([FromBody]UserResponds updatedUser)
        {
            var oldUser = GetCurrentUser()!;
            oldUser.UserName = updatedUser.UserName;
            oldUser.FirstName = updatedUser.FirstName;
            oldUser.LastName = updatedUser.LastName;
            oldUser.Email = updatedUser.Email;
            var result = _userService.UpdateUser(oldUser);
            return result is null ? NotFound() : Ok(new UserResponds(result));
        }

        [HttpPut("UpdateEmail")]
        [ProducesResponseType(typeof(User),200), ProducesResponseType(404)]
        public IActionResult UpdateEmail([FromHeader] string email)
        {
            var result = _userService.UpdateEmail(GetCurrentUser().UserName,email);
            return result is null ? NotFound() : Ok(result);
        }
        #endregion

        [HttpGet("UsernameInUse/{username}")]
        [AllowAnonymous, ProducesResponseType(typeof(string),200)]
        public IActionResult UsernameInUse([FromRoute] string username)
        {
            return _userService.UsernameInUse(username)?Ok("Username is not in use."): Ok("Username is already in use.");
        }

        [HttpDelete("Remove")]
        public IActionResult RemoveAccount()
        {

            var current = GetCurrentUser();
            if (current is null)
            {
                return Unauthorized();
            }
            _userService.Delete(current.UserName);
            return Ok();
        }

        [HttpPut("UpdateUsername")]
        public IActionResult UpdateUsername([FromHeader] string newUsername)
        {
            var result = _userService.UpdateUserName(GetCurrentUser().UserName, newUsername);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("UpdatePrivacy")]

        public IActionResult UpdateNamePrivacy([FromHeader] bool anonymous)
        {
            var user = GetCurrentUser();
            if (user.Anonymous == anonymous) return Ok();
            var result = _userService.UpdatePrivacyMode(user.UserName,anonymous);
            if (result != null) return Ok();
            return BadRequest("something went wrong");
        }
        private User GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null!;
            var userClaims = identity.Claims;
            return _userService.GetUser(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value!)!;

        }

    }
}
