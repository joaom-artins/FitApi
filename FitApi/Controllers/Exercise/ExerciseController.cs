using Fit.Application.DTOs.Requests.Exercise;
using Fit.Application.Interfaces.IServices.Exercise;
using Microsoft.AspNetCore.Mvc;

namespace FitApi.Controllers.Exercise;

[ApiController]
[Route("v1/workouts/{workoutId}/exercises")]
public class ExerciseController(
    IExerciseService _exerciseService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddToWorkout([FromRoute] Guid workoutId, [FromBody] ExerciseCreateRequest request)
    {
        await _exerciseService.AddToWorkoutAsync(workoutId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveToWorkot([FromRoute] Guid id, [FromRoute] Guid workoutId)
    {
        await _exerciseService.RemoveToWorkoutAsync(id, workoutId);

        return NoContent();
    }
}
