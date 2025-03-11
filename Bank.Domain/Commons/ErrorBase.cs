using FluentResults;

namespace Bank.Domain.Commons;

public class ErrorBase : Error
{

    public int Code { get; init; }

    public ErrorBase(int code, string message)
    {
        Code = code;
        Message = message;
    }
}
