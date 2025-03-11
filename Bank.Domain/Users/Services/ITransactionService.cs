using Bank.Domain.Users.Entities;
using Bank.Domain.Users.Entities.ValueObjects;
using FluentResults;

namespace Bank.Domain.Users.Services;

public interface ITransactionService
{

    public Task<Result> MakeTransaction(User user,Account account, Operation operation, CancellationToken token = default);

}
