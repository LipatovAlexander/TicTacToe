using Application.Common.Interfaces;

namespace Application.Commands.CreateGame;

public sealed class CreateGameCommand : ICommand<CreateGameCommandResult>
{
    public required int UserId { get; set; }
}