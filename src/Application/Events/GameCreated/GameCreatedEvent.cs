using Application.Common.Interfaces;

namespace Application.Events.GameCreated;

public sealed class GameCreatedEvent : IEvent
{
    public required int GameId { get; set; }
    public required int HostUserId { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}