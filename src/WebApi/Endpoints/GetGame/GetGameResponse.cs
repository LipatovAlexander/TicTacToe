using WebApi.Common.Models;

namespace WebApi.Endpoints.GetGame;

public sealed class GetGameResponse
{
    public required GameDto Game { get; set; }
}