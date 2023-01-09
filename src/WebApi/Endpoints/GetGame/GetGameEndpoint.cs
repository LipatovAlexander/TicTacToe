using Application.Queries.GetGame;
using MassTransit;
using WebApi.Common;
using WebApi.Common.Mappers;

namespace WebApi.Endpoints.GetGame;

public sealed class GetGameEndpoint : IEndpoint<GetGameResponse, GetGameRequest>
{
    private readonly IRequestClient<GetGameQuery> _requestClient;

    public GetGameEndpoint(IRequestClient<GetGameQuery> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<GetGameResponse> HandleAsync(GetGameRequest request)
    {
        var query = new GetGameQuery
        {
            GameId = request.Id
        };

        var response = await _requestClient.GetResponse<GetGameQueryResult>(query);

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