using System.ComponentModel.DataAnnotations;

namespace Bank.Infrastructure.TransactionService.Rest;

public class RestTransactionOptions
{
    public const string Key = "RestTransaction";

    [Required(AllowEmptyStrings = false)]
    public required string SecretId { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string CreateTokenUri { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string CreateWithdrawUri { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string CreateDepositUri { get; init; }


    // retry pattern options could live here

}
