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
    public async Task<IActionResult> Create([FromBody] WorkoutCreateRequest request)
    {
        await _workoutService.CreateAsync(request);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WorkoutUpdateRequest request)
    {
        await _workoutService.UpdateAsync(id, request);

        return NoContent();
    }
}
