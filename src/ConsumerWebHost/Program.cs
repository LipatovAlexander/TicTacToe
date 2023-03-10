using MassTransit;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
    
        services.AddEvents(configurator => configurator.UsingRabbitMq(configuration));
        services.AddDbContext(configuration);
    })
    .Build()
    .Run();