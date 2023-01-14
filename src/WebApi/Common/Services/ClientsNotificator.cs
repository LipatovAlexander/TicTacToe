using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs.Game;

namespace WebApi.Common.Services;

public sealed class ClientsNotificator : IClientsNotificator
{
    private readonly IHubContext<GameHub, IGameClient> _hubContext;

    public ClientsNotificator(IHubContext<GameHub, IGameClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyGameStartedAsync(Game game)
    {
        await _hubContext.Clients.User(game.Host.UserId.ToString()).StartGame(game.Opponent!.User.UserName!);
        await _hubContext.Clients.User(game.Opponent.UserId.ToString()).StartGame(game.Host.User.UserName!);
    }

    public async Task NotifyMove(Game game, int x, int y)
    {
        var mark = game.Board.Cells[x, y].PlayerMark!.Value;
        var state = game.State;
        
        await _hubContext.Clients.User(game.Host.UserId.ToString()).Move(x, y, mark, state);
        await _hubContext.Clients.User(game.Opponent!.UserId.ToString()).Move(x, y, mark, state);
    }

    public async Task NotifyOpponentDisconnected(Player player, GameState state)
    {
        await _hubContext.Clients.User(player.UserId.ToString()).OpponentDisconnected(state);
    }
}