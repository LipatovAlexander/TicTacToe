using Domain.Entities;

namespace Application.Queries.GetGame;

public sealed class GetGameQueryResult
{
    public required Game Game { get; set; }
}