using Bank.Domain.Users.Services;
using Bank.Infrastructure.TransactionService.Mock;
using Bank.Infrastructure.TransactionService.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Infrastructure.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RestTransactionOptions>(
            configuration.GetSection(RestTransactionOptions.Key));

        services.AddTransient<ITransactionService, MockTransactionClient>();

        // see implementation for mission giver
        // services.AddHttpClient<RestTransactionClient>(); // whatever config you want
        // services.AddTransient<ITransactionService, RestTransactionClient>();

        return services;
    }

}
