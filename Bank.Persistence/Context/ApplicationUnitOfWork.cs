using Bank.Domain.Commons;
using FluentResults;

namespace Bank.Persistence.Context;

internal class ApplicationUnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public ApplicationUnitOfWork(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result> Commit()
    {
        try
        {
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception e)
        {
            return ApplicationErrors.CommitFailed()
                    .CausedBy(e);
        }
    }

    public async Task<Result> Rollback()
    {
        try
        {
            await _context.DisposeAsync();
       
            return Result.Ok();
        }
        catch (Exception e)
        {
            return ApplicationErrors.RollbackFailed()
                    .CausedBy(e);
        }
    }
}
