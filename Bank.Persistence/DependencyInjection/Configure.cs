using Bank.Domain.Commons;
using Bank.Domain.Users;
using Bank.Persistence.Context;
using Bank.Persistence.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistence.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigurePersistence(this IServiceCollection services)
    {

        services.AddDbContext<ApplicationContext>();
        services.AddTransient<IUnitOfWork, ApplicationUnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();


        return services;
    }

}
