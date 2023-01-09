using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddMassTransit(cfg =>
{
    cfg.UsingApplicationBus(configuration);
});

services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

app.Run();