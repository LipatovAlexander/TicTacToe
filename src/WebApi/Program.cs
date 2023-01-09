using Infrastructure.Configurations.Settings;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddMassTransit(cfg =>
{
    var busSettings = configuration.GetSettings<BusSettings>();

    cfg.UsingApplicationBus(busSettings);
});
services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

app.Run();