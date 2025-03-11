using Bank.Domain.Commons;
using Bank.Domain.Users.ValueObjects;
using FluentResults;

namespace Bank.Domain.Users;

public static class UserErrors
{

    public static ErrorBase TzIsInvalid() => new ErrorBase(1, "tz is invalid");
    public static ErrorBase FullNameIsInvalid() => new ErrorBase(2, "full name is invalid");
    public static ErrorBase EnglishFullNameIsInvalid() => new ErrorBase(3, "full name is invalid");
    public static ErrorBase DateOfBirthIsInvalid() => new ErrorBase(4, "date of birth is invalid");
    public static ErrorBase PasswordIsInvalid() => new ErrorBase(5, "password is invalid");
    public static ErrorBase OperationIsInvalid() => new ErrorBase(6, "operation is invalid");
    public static ErrorBase UserIdNotProvided() => new ErrorBase(7, "user id not provided");
    public static ErrorBase UserAlreadyExists() => new ErrorBase(8, "user already exists");
    public static ErrorBase CannotWithdrawMoreThanAccountHolds() => new ErrorBase(9, "cannot withdraw more than account holds");
    public static ErrorBase UnknownTransactionError() => new ErrorBase(10, "unknown transaction error");
    public static ErrorBase AccountIsInvalid() => new ErrorBase(11, "account is invalid");
    public static ErrorBase PasswordMaxLength() => new ErrorBase(12, "password is too long");
    public static ErrorBase PasswordMinLength() => new ErrorBase(13, "password is too short");
}
