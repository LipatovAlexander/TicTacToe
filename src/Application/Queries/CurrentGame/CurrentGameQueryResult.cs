using Domain.Enums;

namespace Application.Queries.CurrentGame;

public sealed class CurrentGameQueryResult
{
    public required PlayerMark?[][] Board { get; set; }
    public required PlayerMark Mark { get; set; }
    public required string? OpponentUsername { get; set; }
    public required GameState State { get; set; }
}