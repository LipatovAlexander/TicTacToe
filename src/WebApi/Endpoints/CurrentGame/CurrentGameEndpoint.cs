using Application;
using Application.Queries.CurrentGame;
using WebApi.Common;
using WebApi.Extensions;

namespace WebApi.Endpoints.CurrentGame;

public sealed class CurrentGameEndpoint : IEndpoint<HttpContext, CurrentGameResponse>
{
    private readonly IApplicationMediator _applicationMediator;

    public CurrentGameEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<CurrentGameResponse>> HandleAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        var query = new CurrentGameQuery
        {
            UserId = httpContext.User.GetUserId()
        };

        var result = await _applicationMediator.Query<CurrentGameQuery, CurrentGameQueryResult>(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return Response.Failure<CurrentGameResponse>(result.Errors);
        }

        return Response.Success(new CurrentGameResponse
        {
            Board = result.Data.Board,
            Mark = result.Data.Mark,
            State = result.Data.State,
            OpponentUsername = result.Data.OpponentUsername
        });
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/currentGame", HandleAsync);
    }
}