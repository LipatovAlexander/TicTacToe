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

        var result = await _applicationMediator.Command<ConnectPlayerCommand, ConnectPlayerCommandResult>(command);

        if (!result.IsSuccessful)
        {
            return;
        }

        if (result.Data.GameStarted)
        {
            await Clients.User(result.Data.HostUserId.ToString()).StartGame(result.Data.OpponentUsername);
            await Clients.User(result.Data.OpponentUserId.ToString()).StartGame(result.Data.HostUsername);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User!.GetUserId();

        var command = new DisconnectPlayerCommand
        {
            UserId = userId
        };

        var result = await _applicationMediator.Command<DisconnectPlayerCommand, DisconnectPlayerCommandResult>(command);

        if (!result.IsSuccessful)
        {
            return;
        }

        if (result.Data.OpponentWon)
        {
            await Clients.User(result.Data.OpponentUserId.ToString()).OpponentDisconnected(result.Data.State);
        }
    }

    public Task Move(int x, int y)
    {
        throw new NotImplementedException();
    }
}