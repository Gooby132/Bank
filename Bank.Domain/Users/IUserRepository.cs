using Bank.Domain.Users.ValueObjects;
using FluentResults;

namespace Bank.Domain.Users;

public interface IUserRepository
{

    public Task<Result<User?>> GetUser(Tz? tz, CancellationToken cancellationToken = default);
    public Task<Result> RegisterUser(User user, CancellationToken cancellationToken = default);
    public Result Update(User user);

}
