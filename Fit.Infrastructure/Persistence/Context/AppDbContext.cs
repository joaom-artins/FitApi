using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fit.Domain.Entities;

namespace Fit.Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>(options)
{
    public override DbSet<UserModel> Users { get; set; } = default!;
    public DbSet<UserRefreshTokenModel> UserRefreshTokens { get; set; } = default!;
    public DbSet<WorkoutModel> Workouts { get; set; } = default!;
    public DbSet<ExerciseModel> Excersises { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkoutModel>()
        .HasOne(w => w.CreatedBy)
        .WithMany()
        .HasForeignKey(w => w.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WorkoutModel>()
            .HasOne(w => w.For)
            .WithMany()
            .HasForeignKey(w => w.ForId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserModel>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RolesClaim");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UsersRoles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UsersTokens");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
    }
}
