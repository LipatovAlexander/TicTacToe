using Domain.Enums;

namespace Domain.Entities;

public sealed class Game : BaseEntity
{
    public required Player Player1 { get; set; }
    
    public required Player Player2 { get; set; }

    public required Board Board { get; set; }

    public required GameState State { get; set; }
}