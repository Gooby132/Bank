using FluentResults;
using System.Text.RegularExpressions;

namespace Bank.Domain.Users.ValueObjects;

public class FullName
{
    public static readonly Regex Validation = new Regex(@"^[\u0590-\u05FF\uFB1D-\uFB4F\s\-']+$");
    public const int MaxCharacters = 20;
    public const int MinCharacters = 2;

    public required string Value { get; init; }

    private FullName() { }

    public static Result<FullName> Create(string? value)
    {

        // errors can be more verbose per statement

        if (string.IsNullOrEmpty(value))
            return UserErrors.FullNameIsInvalid();

        if (!Validation.IsMatch(value))
            return UserErrors.FullNameIsInvalid();

        if (value.Length > MaxCharacters)
            return UserErrors.FullNameIsInvalid();

        if (value.Length < MinCharacters)
            return UserErrors.FullNameIsInvalid();

        return new FullName
        {
            Value = value,
        };
    }

}
