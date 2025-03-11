using Bank.API.Contracts.Commons.Dtos;
using Bank.API.Contracts.Users.Dtos;

namespace Bank.API.Contracts.Users;

public class UserMakeTransactionResponse
{

    public IEnumerable<ErrorDto>? Errors { get; init; }
    public AccountDto? UpdatedAccount { get; init; }

}
