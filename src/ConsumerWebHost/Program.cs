using Infrastructure.Configurations.Settings;
using MassTransit;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(cfg =>
        {
            var busSettings = context.Configuration.GetSettings<BusSettings>();

            cfg.AddApplicationCommands();

            cfg.UsingApplicationBus(busSettings);
        });
    })
    .Build()
    .Run();