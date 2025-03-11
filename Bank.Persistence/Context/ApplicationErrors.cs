using Bank.Domain.Commons;

namespace Bank.Persistence.Context;

public static class ApplicationErrors
{

    public static ErrorBase CommitFailed() => new ErrorBase(1, "commit failed");
    public static ErrorBase RollbackFailed() => new ErrorBase(2, "rollback failed");
    public static ErrorBase RepositoryFailed() => new ErrorBase(3, "repository failed");

}
