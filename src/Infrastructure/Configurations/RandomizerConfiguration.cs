using Application.Common.Interfaces;
using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RandomizerConfiguration
{
    public static IServiceCollection AddRandomizer(this IServiceCollection services)
    {
        return services.AddSingleton<IRandomizer, Randomizer>();
    }
}