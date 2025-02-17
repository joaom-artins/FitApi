using System.ComponentModel.DataAnnotations;
using Fit.Domain.Enums;

namespace Fit.Domain.Entities;

public class WorkoutModel
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Name { get; set; } = string.Empty;
    public UserModel CreatedBy { get; set; } = default!;
    public Guid CreatedById { get; set; }
    public UserModel For { get; set; } = default!;
    public Guid ForId { get; set; }
    public DaysOfWeekEnum DaysOfWeek { get; set; }
}
