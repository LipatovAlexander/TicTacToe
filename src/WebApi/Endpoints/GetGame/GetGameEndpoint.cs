using Application.Queries.GetGame;
using MassTransit;
using WebApi.Common;
using WebApi.Common.Mappers;

namespace WebApi.Endpoints.GetGame;

public sealed class GetGameEndpoint : IEndpoint<GetGameRequest, GetGameResponse>
{
    private readonly IBus _bus;

    public GetGameEndpoint(IBus bus)
    {
        _bus = bus;
    }

    public async Task<GetGameResponse> HandleAsync(GetGameRequest request)
    {
        var query = new GetGameQuery
        {
            GameId = request.Id
        };

        var response = await _bus.Request<GetGameQuery, GetGameQueryResult>(query);

        var gameDto = response.Message.Game.MapToDto();
        return new GetGameResponse
        {
            Game = gameDto
        };
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/games/{id}", async ([AsParameters] GetGameRequest request) => await HandleAsync(request));
    }
}