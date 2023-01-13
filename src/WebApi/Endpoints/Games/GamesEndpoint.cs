using Application;
using Application.Queries.Games;
using WebApi.Common;

namespace WebApi.Endpoints.Games;

public sealed class GamesEndpoint : IEndpoint<List<GameResponse>>
{
    private readonly IApplicationMediator _applicationMediator;

    public GamesEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<List<GameResponse>>> HandleAsync(CancellationToken cancellationToken)
    {
        var query = new GamesQuery();

        var result = await _applicationMediator.Query<GamesQuery, GamesQueryResult>(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return Response.Failure<List<GameResponse>>(result.Errors);
        }

        var responses = result.Data.Games.Select(g => new GameResponse
        {
            Id = g.Id,
            CreatedAt = g.CreatedAt,
            HostUsername = g.HostUsername
        }).ToList();

        return Response.Success(responses);

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/games", HandleAsync);
    }
}