using Application;
using Application.Commands.CreateGame;
using WebApi.Common;
using WebApi.Extensions;

namespace WebApi.Endpoints.CreateGame;

public sealed class CreateGameEndpoint : IEndpoint<HttpContext, CreateGameResponse>
{
    private readonly IApplicationMediator _applicationMediator;

    public CreateGameEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<CreateGameResponse>> HandleAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        var command = new CreateGameCommand
        {
            UserId = httpContext.User.GetUserId()
        };

        var result = await _applicationMediator.Command<CreateGameCommand, CreateGameCommandResult>(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return Response.Failure<CreateGameResponse>(result.Errors);
        }

        return Response.Success(new CreateGameResponse
        {
            Id = result.Data.GameId
        });
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", HandleAsync);
    }
}