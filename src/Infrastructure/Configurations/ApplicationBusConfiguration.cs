using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace MassTransit;

public static class ApplicationBusConfiguration
{
    public static IBusRegistrationConfigurator UsingApplicationBus(this IBusRegistrationConfigurator configurator, IConfiguration configuration)
    {
        var busSettings = configuration.GetSettings<BusSettings>();

        configurator.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(busSettings.Host, "/", h =>
            {
                h.Username(busSettings.Username);
                h.Password(busSettings.Password);
            });

            cfg.ConfigureEndpoints(ctx);
        });

        return configurator;
    }
}