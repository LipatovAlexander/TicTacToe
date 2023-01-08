using Domain.Common;

namespace Domain.Entities;

public sealed class Game : BaseEntity
{
    public required Player Player1 { get; set; }
    
    public required Player Player2 { get; set; }

    public Player? Winner { get; set; }

    public required Board Board { get; set; }
}