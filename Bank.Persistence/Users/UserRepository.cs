using Bank.Domain.Users;
using Bank.Domain.Users.ValueObjects;
using Bank.Persistence.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence.Users;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result<User?>> GetUser(Tz? tz, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Tz == tz, cancellationToken);
        }
        catch (Exception e)
        {
            return ApplicationErrors.RepositoryFailed()
                .CausedBy(e);
        }
    }

    public async Task<Result> RegisterUser(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Users.AddAsync(user, cancellationToken);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return ApplicationErrors.RepositoryFailed()
                .CausedBy(e);
        }
    }

    public Result Update(User user)
    {
        try
        {
            _context.Users.Update(user);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return ApplicationErrors.RepositoryFailed()
                .CausedBy(e);
        }
    }
}
