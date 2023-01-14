using Application.Common.Interfaces;
using WebApi.Common.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ClientsNotificatorConfiguration
{
    public static IServiceCollection AddClientsNotificator(this IServiceCollection services)
    {
        return services.AddScoped<IClientsNotificator, ClientsNotificator>();
    }
}