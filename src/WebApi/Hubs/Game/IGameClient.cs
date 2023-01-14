namespace WebApi.Hubs.Game;

public interface IGameClient
{
    Task StartGame(string opponentUsername);
    Task Move(int x, int y, string mark, string state);
    Task OpponentDisconnected(string state);
}