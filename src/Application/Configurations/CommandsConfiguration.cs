using Application;
using FluentValidation;

// ReSharper disable once CheckNamespace
namespace MassTransit;

public static class CommandsConfiguration
{
    public static IBusRegistrationConfigurator AddApplicationCommands(this IBusRegistrationConfigurator configurator)
    {
        var applicationAssembly = typeof(AssemblyMarker).Assembly;

        configurator.AddValidatorsFromAssembly(applicationAssembly);
        configurator.AddConsumers(applicationAssembly);

        return configurator;
    }
}