using Microsoft.EntityFrameworkCore;

namespace Fit.Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}
