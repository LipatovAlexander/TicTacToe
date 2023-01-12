using System.Reflection;
using Application;
using Application.Common;
using Application.Common.Interfaces;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<ICommandBus>(typeof(CommandHandlerBase<,>), typeof(ICommand<>), configure);
        return services;
    }

    public static IServiceCollection AddEvents(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IEventBus>(typeof(EventHandlerBase<>), typeof(IEvent), configure);
        return services;
    }
    
    public static IServiceCollection AddQueries(this IServiceCollection services, Action<IBusRegistrationConfigurator> configure)
    {
        services.AddConsumers<IQueryBus>(typeof(QueryHandlerBase<,>), typeof(IQuery<>), configure);
        return services;
    }
    
    private static void AddConsumers<TBus>(this IServiceCollection services, Type consumerType, Type requestType, Action<IBusRegistrationConfigurator> configure)
        where TBus : class, IBus
    {
        var assembly = typeof(AssemblyMarker).Assembly;

        var consumerTypes = GetTypesAssignableTo(assembly, consumerType);
        var requestTypes = GetTypesAssignableTo(assembly, requestType);

        services.AddMassTransit<TBus>(configurator =>
        {
            configurator.AddConsumers(consumerTypes);
            configure.Invoke(configurator);
            
            foreach (var type in requestTypes)
            {
                configurator.AddRequestClient(type);
            }
        });
    }

    private static Type[] GetTypesAssignableTo(Assembly assembly, Type type)
    {
        return assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && (t.IsAssignableTo(type) || IsAssignableToGenericType(t, type)))
            .ToArray();
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        var baseType = givenType.BaseType;
        
        return baseType != null && IsAssignableToGenericType(baseType, genericType);
    }
}