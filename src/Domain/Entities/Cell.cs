using Domain.Common;

namespace Domain.Entities;

public sealed class Cell : BaseEntity
{
    public required int X { get; set; }
    
    public required int Y { get; set; }

    public Player? Player { get; set; }
}