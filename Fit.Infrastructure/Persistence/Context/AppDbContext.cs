using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fit.Domain.Entities;

namespace Fit.Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>(options)
{
    public override DbSet<UserEntity> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RolesClaim");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UsersRoles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UsersTokens");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
    }
}
