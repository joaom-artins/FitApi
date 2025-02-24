using Fit.Application.DTOs.Requests.Exercise;
using Fit.Application.Interfaces.IServices.Exercise;
using Microsoft.AspNetCore.Mvc;

namespace Fit.API.Controllers.Exercise;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] Guid workoutId, [FromBody] ExerciseUpdateRequest request)
    {
        await _exerciseService.UpdateAsync(id, workoutId, request);

        return NoContent();
    }

    [HttpPatch("change-order")]
    public async Task<IActionResult> ChangeOrder([FromRoute] Guid workoutId, [FromBody] ExerciseChangeOrderRequest request)
    {
        await _exerciseService.ChangeOrderAsync(workoutId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveToWorkot([FromRoute] Guid id, [FromRoute] Guid workoutId)
    {
        await _exerciseService.RemoveToWorkoutAsync(id, workoutId);

        return NoContent();
    }
}
