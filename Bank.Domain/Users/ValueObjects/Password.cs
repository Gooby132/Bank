using FluentResults;

namespace Bank.Domain.Users.ValueObjects;

public class Password
{
    public const int MaxPasswordLength = 12;
    public const int MinPasswordLength = 8;

    public required string Value { get; init; }

    private Password() { }

    public static Result<Password> Create(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return UserErrors.PasswordIsInvalid();

        if (value.Length > MaxPasswordLength)
            return UserErrors.PasswordIsInvalid();

        if (value.Length < MinPasswordLength)
            return UserErrors.PasswordIsInvalid();

        return new Password { Value = value };

    }

    public bool Validate(string? password) => password == Value;

}
