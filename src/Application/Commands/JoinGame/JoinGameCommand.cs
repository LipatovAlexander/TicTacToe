using Application.Common.Interfaces;

namespace Application.Commands.JoinGame;

public sealed class JoinGameCommand : ICommand<JoinGameCommandResult>
{
    public required int UserId { get; set; }
    public required int GameId { get; set; }
}