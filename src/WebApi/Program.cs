using Application.Common.Interfaces;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCommands(configurator => configurator.UsingInMemory());
services.AddQueries(configurator => configurator.UsingInMemory());
services.AddMassTransit<IEventBus>(configurator => configurator.UsingRabbitMq(configuration));
services.AddApplicationMediator();
services.AddDbContext(configuration);

services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

app.Run();