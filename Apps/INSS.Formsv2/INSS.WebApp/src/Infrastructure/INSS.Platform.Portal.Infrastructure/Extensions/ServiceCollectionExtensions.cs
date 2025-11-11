using INSS.Platform.Portal.Application.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace INSS.Platform.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserSessionResolver, TestUserSessionResolver>();
        return services;
    }
}