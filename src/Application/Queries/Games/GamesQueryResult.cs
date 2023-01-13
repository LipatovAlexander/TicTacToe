namespace Application.Queries.Games;

public sealed class GamesQueryResult
{
    public required List<GameDto> Games { get; set; }
}

public class GameDto
{
    public required int Id { get; set; }
    public required string HostUsername { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}