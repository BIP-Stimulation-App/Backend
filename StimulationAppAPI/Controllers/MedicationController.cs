using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.DAL.Model.Request;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;
        private readonly IUserService _userService;

        public MedicationController(MedicationService medicationService, UserService userService)
        {
            _medicationService = medicationService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetMedications()//returns a list of medications
        {
            var user = GetCurrentUser()!.UserName;
            var newMedications = _medicationService.GetMedications(user).Select(u => new
            {
               id = u.Id,
               name = u.Name,
               description = u.Description,
               frequency = u.Frequency,
               time = u.Time
            }).ToList();
            return Ok(newMedications);
        }

        [HttpGet("{id}")]
        public IActionResult GetMedicationById([FromRoute] int id) //returns NotFound or NewMedication
        {
            var medication = _medicationService.GetById(id);
            return medication is null? NotFound(): Ok(new NewMedication(medication));
        }

        [HttpPost]
        public IActionResult AddMedication([FromBody]NewMedication newMedication)
        {
            var user = GetCurrentUser()!;
            user.Medications ??= new List<Medication>();
            user.Medications.Add(new Medication(newMedication));
            try
            {
                _userService.UpdateUser(user);
                return Ok();
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        } //returns OK or Error

        [HttpDelete("{id}")]
        public IActionResult DeleteMedication([FromRoute]int id) //returns not found, forbid, Ok or problem
        {
            var user = GetCurrentUser()!;
            try
            {
                var result = _medicationService.GetById(id);
                if (result is null)
                {
                    return NotFound();
                }
                if (result.Dependence!= user.UserName)
                {
                    return Forbid("You do not own this medication");
                }
                user.Medications.Remove(result);
                _userService.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        private User? GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims;
            return _userService.GetUser(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value!);

        }

    }
}
