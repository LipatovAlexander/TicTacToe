using Infrastructure.Common;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSettings<TSettings>(this IServiceCollection services, IConfiguration configuration)
        where TSettings: class, ISettings
    {
        services.Configure<TSettings>(configuration.GetSection(TSettings.SectionName));
    }
}