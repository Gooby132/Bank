namespace Bank.API.Contracts.Users.Dtos;

public class AccountDto
{

    public required int Id { get; init; }
    public required int Currency { get; init; }
    public required IEnumerable<OperationDto> Operations { get; init; }

}
