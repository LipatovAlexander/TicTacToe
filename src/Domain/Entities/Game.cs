using Domain.Enums;

namespace Domain.Entities;

public sealed class Game : BaseEntity
{
    public Player NoughtsPlayer { get; set; } = default!;
    public Player CrossesPlayer { get; set; } = default!;
    public required GameState State { get; set; }
    public required Board Board { get; set; }
}