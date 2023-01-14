using Application;
using Application.Commands.ConnectPlayer;
using Application.Commands.DisconnectPlayer;
using Microsoft.AspNetCore.SignalR;
using WebApi.Extensions;

namespace WebApi.Hubs.Game;

public sealed class GameHub : Hub<IGameClient>
{
    private readonly IApplicationMediator _applicationMediator;

    public GameHub(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User!.GetUserId();

        var command = new ConnectPlayerCommand
        {
            UserId = userId
        };

        await _applicationMediator.Command<ConnectPlayerCommand, ConnectPlayerCommandResult>(command);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User!.GetUserId();

        var command = new DisconnectPlayerCommand
        {
            UserId = userId
        };

        await _applicationMediator.Command<DisconnectPlayerCommand, DisconnectPlayerCommandResult>(command);
    }

    public Task Move(int x, int y)
    {
        throw new NotImplementedException();
    }
}