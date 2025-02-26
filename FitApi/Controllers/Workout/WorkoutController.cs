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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _workoutService.GetByIdAsync(id);

        return Ok(result);
    }

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
