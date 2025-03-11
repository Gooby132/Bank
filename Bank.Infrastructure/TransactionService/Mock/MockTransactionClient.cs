using Bank.Domain.Users;
using Bank.Domain.Users.Entities;
using Bank.Domain.Users.Entities.ValueObjects;
using Bank.Domain.Users.Services;
using FluentResults;

namespace Bank.Infrastructure.TransactionService.Mock;

public class MockTransactionClient : ITransactionService
{
    public async Task<Result> MakeTransaction(User user, Account account, Operation operation, CancellationToken token = default)
    {
        if (Random.Shared.Next(0, 5) == 0)
            return Result.Fail("failure");

        return Result.Ok();
    }
}
