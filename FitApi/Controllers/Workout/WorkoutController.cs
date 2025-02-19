using Fit.Application.DTOs.Requests.Workout;
using Fit.Application.Interfaces.IServices.Workout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fit.Api.Controllers.Workout;

[ApiController]
[Route("v1/workout")]
public class WorkoutController(
    IWorkoutService _workoutService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] WorkoutCreateRequest request)
    {
        await _workoutService.CreateAsync(request);

        return NoContent();
    }
}
