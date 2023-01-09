using Domain.Enums;

namespace Application.Commands.Move;

public sealed class MoveCommandResult
{
    public required GameState State { get; set; }
}