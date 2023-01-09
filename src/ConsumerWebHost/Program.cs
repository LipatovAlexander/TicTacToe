using MassTransit;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddApplicationCommands();

            cfg.UsingApplicationBus(context.Configuration);
        });
    })
    .Build()
    .Run();