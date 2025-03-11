using Bank.API.Contracts.Users.Dtos;

namespace Bank.API.Contracts.Users;

public class UserRegisterRequest
{

    public required UserDto? User { get; init; }
    public string? Password { get; init; }

}
