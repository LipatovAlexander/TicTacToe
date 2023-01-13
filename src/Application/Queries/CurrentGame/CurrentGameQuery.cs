using Application.Common.Interfaces;

namespace Application.Queries.CurrentGame;

public sealed class CurrentGameQuery : IQuery<CurrentGameQueryResult>
{
    public required int UserId { get; set; }
}