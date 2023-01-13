using Application.Common.Interfaces;
using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class JwtGeneratorConfiguration
{
    public static IServiceCollection AddJwtGenerator(this IServiceCollection services)
    {
        return services.AddSingleton<IJwtGenerator, JwtGenerator>();
    }
}