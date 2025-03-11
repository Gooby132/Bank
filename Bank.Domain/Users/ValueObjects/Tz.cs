using Bank.Domain.Commons;
using FluentResults;
using System.Text.RegularExpressions;

namespace Bank.Domain.Users.ValueObjects;

public class Tz : IValueObject
{

    public const int ValidLength = 9;
    public readonly static Regex Validation = new Regex(@"^\d{9}$");

    public required string Value { get; init; }

    private Tz() { }

    public static Result<Tz> Create(string? value)
    {
        if(string.IsNullOrEmpty(value))
            return UserErrors.TzIsInvalid();

        if(value.Length != ValidLength)
            return UserErrors.TzIsInvalid();

        if (!Validation.IsMatch(value))
            return UserErrors.TzIsInvalid();

        return new Tz { Value = value };
    }

    private bool Equals(Tz other)
        => Value == other.Value;

    public override bool Equals(object obj)
        => ReferenceEquals(this, obj) || obj is Tz other && Equals(other);

    public override int GetHashCode()
        => Value.GetHashCode();
}
