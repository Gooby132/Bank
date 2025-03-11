namespace Bank.API.Contracts.Users.Dtos;

public class OperationDto
{

    public required int Value { get; init; }
    public required int Type { get; init; }
    public int SentTo { get; init; }

}
