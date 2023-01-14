using Domain.Enums;

namespace WebApi.Hubs.Game;

public interface IGameClient
{
    Task StartGame(string opponentUsername);
    Task Move(int x, int y, PlayerMark mark, GameState state);
    Task OpponentDisconnected(GameState state);
}