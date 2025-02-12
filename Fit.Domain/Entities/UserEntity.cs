﻿using System.ComponentModel.DataAnnotations;
using Fit.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fit.Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public Guid? PersonalId { get; set; }
    public UserEntity? PersonalTrainer { get; set; }
    public UserTypeEnum Type { get; set; }
}
