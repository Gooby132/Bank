using Bank.Domain.Users.Entities.ValueObjects;
using FluentResults;

namespace Bank.Domain.Users.Entities;

public class Account
{
    public const int MinAccountId = 0;
    public const int MaxAccountId = 1_000_000_000;

    public int Id { get; init; } = Random.Shared.Next(MinAccountId, MaxAccountId);
    public int Currency { get; private set; }
    public ICollection<Operation> Operations { get; init; } = new List<Operation>();

    private Account() { }

    public static Account CreateRandom() => new Account()
    {
        Currency = Random.Shared.Next(0, 100),
    };

    public Result MakeTransaction(Operation operation)
    {
        switch (operation.Type)
        {
            case Operation.OperationType.Deposit:
                Currency += operation.Value;
                Operations.Add(operation);
                return Result.Ok();
            case Operation.OperationType.Withdraw:
                if (Currency - operation.Value < 0)
                    return UserErrors.CannotWithdrawMoreThanAccountHolds();
                Currency -= operation.Value;
                Operations.Add(operation);
                return Result.Ok();
            default:
                return Result.Fail(UserErrors.UnknownTransactionError());
        }
    }

}
