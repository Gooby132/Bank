namespace Bank.API.Contracts.Users.Dtos;

public class UserDto
{

    public string? Tz { get; init; }
    public string? FullName { get; init; }
    public string? EnglishFullName { get; init; }
    public string? BirthDateInUtc { get; init; }
    public AccountDto? Account { get; init; }
    public string? Password { get; init; }

}
