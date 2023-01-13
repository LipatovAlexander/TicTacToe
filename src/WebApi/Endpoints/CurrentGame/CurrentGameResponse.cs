using System.Text.Json.Serialization;
using Domain.Enums;

namespace WebApi.Endpoints.CurrentGame;

public sealed class CurrentGameResponse
{
    public required PlayerMark?[][] Board { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required PlayerMark Mark { get; set; }

    public required string? OpponentUsername { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required GameState State { get; set; }
}