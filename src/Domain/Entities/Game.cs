using Domain.Enums;

namespace Domain.Entities;

public sealed class Game : BaseEntity
{
    public Player Host { get; set; } = default!;
    public Player? Opponent { get; set; }
    public required GameState State { get; set; }
    public required Board Board { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}