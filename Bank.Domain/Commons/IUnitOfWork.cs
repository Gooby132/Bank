using FluentResults;

namespace Bank.Domain.Commons;

public interface IUnitOfWork
{

    public Task<Result> Commit();
    public Task<Result> Rollback();

}
