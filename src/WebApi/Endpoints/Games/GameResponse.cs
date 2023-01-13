namespace WebApi.Endpoints.Games;

public sealed class GameResponse
{
    public required int Id { get; set; }
    public required string HostUsername { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}