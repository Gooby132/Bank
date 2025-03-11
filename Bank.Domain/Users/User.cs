using Bank.Domain.Users.Entities;
using Bank.Domain.Users.Entities.ValueObjects;
using Bank.Domain.Users.ValueObjects;
using FluentResults;

namespace Bank.Domain.Users;

public class User
{

    public required Tz Tz { get; init; }
    public required FullName FullName { get; init; }
    public required EnglishFullName EnglishFullName { get; init; }
    public required DateOfBirth DateOfBirthInUtc { get; init; }
    public required Password Password { get; init; } // only for example purposes
    public Account Account { get; init; } = Account.CreateRandom(); // used for presentation

    private User() { }

    public static Result<User> Create(
        string? tz, 
        string? fullName, 
        string? englishFullName, 
        string? dateOfBirthInUtc,
        string? password)
    {

        var errors = new List<IError>();

        var tzResult = Tz.Create(tz);
        var fullNameResult = FullName.Create(fullName);
        var englishFullNameResult = EnglishFullName.Create(englishFullName);
        var dateOfBirthInUtcResult = DateOfBirth.Create(dateOfBirthInUtc);
        var passwordResult = Password.Create(password);

        errors.AddRange(tzResult.Errors);
        errors.AddRange(fullNameResult.Errors);
        errors.AddRange(englishFullNameResult.Errors);
        errors.AddRange(dateOfBirthInUtcResult.Errors);
        errors.AddRange(passwordResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

        return new User
        {
            Tz = tzResult.Value,
            DateOfBirthInUtc = dateOfBirthInUtcResult.Value,
            EnglishFullName = englishFullNameResult.Value,
            FullName = fullNameResult.Value,
            Password = passwordResult.Value,
        };
    }

    public Result MakeTransaction(Operation operation) => Account.MakeTransaction(operation);

    public bool ValidatePassword(string? password) => Password.Validate(password);

}
