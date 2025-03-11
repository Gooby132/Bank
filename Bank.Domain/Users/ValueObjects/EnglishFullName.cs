using FluentResults;
using System.Text.RegularExpressions;

namespace Bank.Domain.Users.ValueObjects;

public class EnglishFullName
{
    public static readonly Regex Validation = new Regex(@"^[A-Za-z\s\-']+$");
    public const int MaxCharacters = 15;
    public const int MinCharacters = 2;

    public required string Value { get; init; }

    private EnglishFullName() { }

    public static Result<EnglishFullName> Create(string? value)
    {

        // errors can be more verbose per statement

        if (string.IsNullOrEmpty(value))
            return UserErrors.EnglishFullNameIsInvalid();

        if (!Validation.IsMatch(value))
            return UserErrors.EnglishFullNameIsInvalid();

        if (value.Length > MaxCharacters)
            return UserErrors.EnglishFullNameIsInvalid();

        if (value.Length < MinCharacters)
            return UserErrors.EnglishFullNameIsInvalid();

        return new EnglishFullName
        {
            Value = value,
        };
    }

}
