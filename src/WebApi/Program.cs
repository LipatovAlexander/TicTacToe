using Application.Common.Interfaces;
using Infrastructure.Configurations;
using MassTransit;
using WebApi.Hubs.Game;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCommands(configurator => configurator.UsingInMemory());
services.AddQueries(configurator => configurator.UsingInMemory());
services.AddMassTransit<IEventBus>(configurator => configurator.UsingRabbitMq(configuration));

services.AddApplicationMediator();
services.AddDbContext(configuration);

services.AddIdentity();
services.AddJwtGenerator();
services.AddJwtAuthentication(configuration);

services.AddRandomizer();
services.AddDateTimeProvider();
services.AddClientsNotificator();

services.AddEndpoints();

var app = builder.Build();

app.UseJwtAuthentication();

app.MapEndpoints();
app.MapHub<GameHub>("/api/game");

app.Run();