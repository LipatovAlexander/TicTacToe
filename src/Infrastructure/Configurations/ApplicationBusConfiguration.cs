using Infrastructure.Configurations.Settings;

// ReSharper disable once CheckNamespace
namespace MassTransit;

public static class ApplicationBusConfiguration
{
    public static IBusRegistrationConfigurator UsingApplicationBus(this IBusRegistrationConfigurator configurator, BusSettings settings)
    {
        configurator.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(settings.Host, "/", h =>
            {
                h.Username(settings.Username);
                h.Password(settings.Password);
            });

            cfg.ConfigureEndpoints(ctx);
        });

        return configurator;
    }
}