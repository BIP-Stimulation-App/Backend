using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        IMedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]
        public IActionResult GetMedications()
        {
            return Ok(_medicationService.GetUserMedications(GetCurrentUser()!));
        }

        [HttpPost]
        public IActionResult AddMedication([FromBody]Medication medication)
        {
            medication.UserName = GetCurrentUser()!;
            try
            {
                _medicationService.AddMedication(medication);
                return Ok();
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteMedication([FromBody] Medication medication)
        {
            medication.UserName = GetCurrentUser()!;
            try
            {
                var result = _medicationService.GetById(medication.Id);
                if (result is null)
                {
                    NotFound();
                }
                if (!string.Equals(result.UserName, medication.UserName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return Forbid("You do not own this medication");
                }
                _medicationService.RemoveMedication(medication.Id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        private string? GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

        }

    }
}
