using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IClientsNotificator
{
    Task NotifyGameStartedAsync(Game game);
    Task NotifyMove(Game game, int x, int y);
    Task NotifyOpponentDisconnected(Player player, GameState state);
}