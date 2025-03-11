using Bank.Domain.Users;
using Bank.Domain.Users.Entities;
using Bank.Domain.Users.Entities.ValueObjects;
using Bank.Domain.Users.Services;
using Bank.Infrastructure.TransactionService.Rest.Contracts;
using FluentResults;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bank.Infrastructure.TransactionService.Rest;

public class RestTransactionClient : ITransactionService
{

    private readonly HttpClient _client;
    private readonly RestTransactionOptions _config;
    private string? _token;

    public RestTransactionClient(HttpClient client, IOptions<RestTransactionOptions> options)
    {
        _client = client;
        _config = options.Value;
    }

    public async Task<Result> CreateToken(User user, CancellationToken token = default)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(_config.CreateTokenUri, new CreateTokenRequest
            {
                SecretId = _config.SecretId,
                UserId = user.Tz.Value
            }, token);

            if (!response.IsSuccessStatusCode)
                return Result.Fail("provider error");

            var json = await response.Content.ReadFromJsonAsync<CreateTokenResponse>(token);

            if (json is null)
                return Result.Fail("contract failed");

            if (json.Code != 200) // success i guess
                return Result.Fail("provider error");

            _token = json.Token;

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("provider error");
        }
    }

    public async Task<Result> MakeTransaction(User user, Account account, Operation operation, CancellationToken token = default)
    {
        try
        {
            var tokenResult = await CreateToken(user, token);

            if (tokenResult.IsFailed)
                return Result.Fail(tokenResult.Errors);

            HttpResponseMessage? result = null;
            switch (operation.Type)
            {
                case Operation.OperationType.Deposit:
                    result = await _client.PostAsJsonAsync(_config.CreateDepositUri, new CreateDepositRequest
                    {

                    });
                    break;
                case Operation.OperationType.Withdraw:
                    result = await _client.PostAsJsonAsync(_config.CreateWithdrawUri, new CreateWithdrawRequest
                    {

                    });
                    break;
            }

            if (result is null || !result.IsSuccessStatusCode)
            {
                return Result.Fail("provider failed making operation");
            }

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("provider failed making operation");
        }
    }
}
