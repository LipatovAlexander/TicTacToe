using Application;
using Application.Commands.ConnectPlayer;
using Application.Commands.DisconnectPlayer;
using Application.Commands.Move;
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
            await Clients.User(result.Data.OpponentUserId.ToString()).OpponentDisconnected(result.Data.State.ToString());
        }
    }

    public async Task Move(int x, int y)
    {
        var command = new MoveCommand
        {
            UserId = Context.User!.GetUserId(),
            X = x,
            Y = y
        };

        var result = await _applicationMediator.Command<MoveCommand, MoveCommandResult>(command);

        if (!result.IsSuccessful)
        {
            return;
        }

        await Clients.Users(result.Data.HostUserId.ToString(), result.Data.OpponentUserId.ToString())
            .Move(result.Data.X, result.Data.Y, result.Data.Mark.ToString(), result.Data.State.ToString());
    }
}