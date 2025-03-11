using FluentResults;

namespace Bank.Domain.Users.Entities.ValueObjects;

public class Operation
{

    public OperationType Type { get; init; }
    public int Value { get; init; }
    public int SentTo { get; init; }

    private Operation() { }

    public static Result<Operation> Create(int typeValue, int value, int sentTo)
    {
        if (!Enum.IsDefined(typeof(OperationType), typeValue))
            return UserErrors.OperationIsInvalid();

        if (value <= 0)
            return UserErrors.OperationIsInvalid();

        if (sentTo <= Account.MinAccountId || sentTo > Account.MaxAccountId)
            return UserErrors.AccountIsInvalid();

        return new Operation
        {
            Type = (OperationType)typeValue,
            Value = value,
            SentTo = sentTo
        };
    }

    public enum OperationType
    {
        Deposit,
        Withdraw
    }
}
