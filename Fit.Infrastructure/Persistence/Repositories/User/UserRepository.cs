using Fit.Application.Interfaces.IRepositories.User;
using Fit.Domain.Entities;
using Fit.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Fit.Infrastructure.Persistence.Repositories.User;

public class UserRepository(
    AppDbContext context
) : GenericRepository<UserModel>(context),
    IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<UserModel>> GetByEmailOrUserNameAsync(string email, string username)
    {
        return await _context.Users.Where(x => x.Email == email || x.UserName == username).ToListAsync();
    }

    public async Task<IEnumerable<UserModel>> GetByEmailOrUserNameExcludingIdAsync(Guid id, string email, string username)
    {
        return await _context.Users.Where(x => x.Id != id && (x.Email == email || x.UserName == username)).ToListAsync();
    }
}
