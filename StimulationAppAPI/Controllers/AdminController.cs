using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.DAL.Model.Requests;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var newResult = _userService.GetUsers();

            var newUserList = newResult.Select(item => new UserResponds(item)).ToList();
            return Ok(newUserList);
        } //returns all users as UserRespond list
        [HttpGet("{userName}")]
        public IActionResult GetUsers(string userName)
        {
            var result = _userService.GetUser(userName);
            if (result is null) return NotFound();
            result.Login = new();//removes important data
            return Ok(new UserResponds(_userService.GetUser(userName)));
        } //returns given user as UserRespond
    }
}
