using Bank.API.Contracts.Commons.Dtos;
using Bank.API.Contracts.Users.Dtos;

namespace Bank.API.Contracts.Users;

public class UserLoginResponse
{

    public UserDto? User { get; init; }
    public IEnumerable<ErrorDto>? Errors { get; init; }

}
