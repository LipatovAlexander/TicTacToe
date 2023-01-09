using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs.Game;

public sealed class GameHub : Hub<IGameClient>
{
}