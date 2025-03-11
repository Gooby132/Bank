namespace Bank.API.Contracts.Users;

public class UserLoginRequest
{
    public string? Tz { get; init; }
    public string? Password { get; init; }
}
