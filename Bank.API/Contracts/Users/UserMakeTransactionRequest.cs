using Bank.API.Contracts.Users.Dtos;

namespace Bank.API.Contracts.Users;

public class UserMakeTransactionRequest
{
    public UserDto? User { get; init; }
    public int SendTo { get; init; }
    public int Amount { get; init; }
    public int OperationType { get; init; }

}
