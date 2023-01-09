using Domain.Entities;
using Domain.Enums;

namespace Application.Commands.Move;

public sealed class MoveCommand
{
    public required int GameId { get; set; }
    public required PlayerMark PlayerMark { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
    
    public Game Game { get; set; } = default!;
}