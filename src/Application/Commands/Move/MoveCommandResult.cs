using Domain.Enums;

namespace Application.Commands.Move;

public sealed class MoveCommandResult
{
    public required int HostUserId { get; set; }
    public required int OpponentUserId { get; set; }

    public required int X { get; set; }
    public required int Y { get; set; }
    public required PlayerMark Mark { get; set; }
    public required GameState State { get; set; }
}