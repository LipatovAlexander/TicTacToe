using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace MassTransit;

public static class RabbitMqConfiguration
{
    public static IBusRegistrationConfigurator UsingRabbitMq(this IBusRegistrationConfigurator configurator, IConfiguration configuration)
    {
        var settings = configuration.GetSettings<RabbitSettings>();

        configurator.UsingRabbitMq((ctx, rabbitMqConfigurator) =>
        {
            rabbitMqConfigurator.Host(settings.Host, "/", hostConfigurator =>
            {
                hostConfigurator.Username(settings.Username);
                hostConfigurator.Password(settings.Password);
            });

            rabbitMqConfigurator.ConfigureEndpoints(ctx);
        });

        return configurator;
    }
}