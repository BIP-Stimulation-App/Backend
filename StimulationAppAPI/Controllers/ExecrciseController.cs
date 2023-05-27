using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExerciseController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult AddExercise([FromBody] Exercise exercise)
        {
            try
            {
                var result = _exerciseService.AddExercise(exercise);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateExercise([FromBody] Exercise exercise)
        {
            try
            {
                var result = _exerciseService.UpdateExercise(exercise);
                if (result is null) return NotFound($"Couldn't find exercise with id: {exercise.Id}");
                return Ok(exercise);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteExercise(int id)
        {
            try
            {
                var exercise = _exerciseService.GetExercise(id);
                if (exercise is null) return NotFound($"Couldn't find exercise with id: {id}");
                var result = _exerciseService.RemoveExercise(exercise);
                if (result is null) return Problem("A problem occurred, please try again");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetExercises()
        {
            try
            {
                return Ok(_exerciseService.GetExercises());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetExercise(int id)
        {
            try
            {
                var result = _exerciseService.GetExercise(id);
                if (result is null) return NotFound($"Couldn't find exercise with id: {id}");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
