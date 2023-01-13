using Application;
using Application.Commands.JoinGame;
using WebApi.Common;
using WebApi.Extensions;

namespace WebApi.Endpoints.JoinGame;

public sealed class JoinGameEndpoint : IEndpoint<JoinGameRequest, HttpContext, JoinGameResponse>
{
    private readonly IApplicationMediator _applicationMediator;

    public JoinGameEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<JoinGameResponse>> HandleAsync(JoinGameRequest request, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var command = new JoinGameCommand
        {
            UserId = httpContext.User.GetUserId(),
            GameId = request.GameId
        };

        var result =
            await _applicationMediator.Command<JoinGameCommand, JoinGameCommandResult>(command, cancellationToken);

        return result.IsSuccessful
            ? Response.Success(new JoinGameResponse())
            : Response.Failure<JoinGameResponse>(result.Errors);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/join", HandleAsync);
    }
}