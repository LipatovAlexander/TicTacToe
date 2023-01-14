using Application.Common.Interfaces;

namespace Application.Commands.Move;

public sealed class MoveCommand : ICommand<MoveCommandResult>
{
    public required int UserId { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
}