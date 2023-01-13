using Application;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}