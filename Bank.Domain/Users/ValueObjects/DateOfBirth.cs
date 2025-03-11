using FluentResults;
using System.Globalization;

namespace Bank.Domain.Users.ValueObjects;

public class DateOfBirth
{

    public required DateTime Value { get; init; }

    private DateOfBirth () { }

    public static Result<DateOfBirth> Create(string? value)
    {
        if(string.IsNullOrEmpty(value))
            return UserErrors.DateOfBirthIsInvalid();

        if (!DateTime.TryParse(value,null, DateTimeStyles.AdjustToUniversal, out var res))
            return UserErrors.DateOfBirthIsInvalid();

        if(res >= DateTime.UtcNow)
            return UserErrors.DateOfBirthIsInvalid();

        return new DateOfBirth
        {
            Value = res
        };
    }

}
