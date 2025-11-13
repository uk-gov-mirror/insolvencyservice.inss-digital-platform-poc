using INSS.Platform.Portal.Application.Clients;
using INSS.Platform.Portal.Application.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace INSS.Platform.Portal.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserSessionResolver, TestUserSessionResolver>();
        services.AddHttpClient<IBankAccountClient, BankAccountClient>(client =>
        {
            client.BaseAddress = new Uri("https://vseries.bottomline.com/api/");
        });
        return services;
    }
}