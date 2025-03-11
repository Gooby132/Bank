namespace Bank.Infrastructure.TransactionService.Rest.Contracts;

internal class CreateTokenRequest
{
    public string? UserId { get; init; }
    public string? SecretId { get; init; }
}
