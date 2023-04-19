using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminController(UserService userService)
        {
            _userService = userService;
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }
        [HttpGet("{userName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers(string userName)
        {
            return Ok(_userService.GetUser(userName));
        }
    }
}
