using Application.Common.Interfaces;

namespace Application.Events.GameStarted;

public sealed class GameStartedEvent : IEvent
{
    public required int GameId { get; set; }
    public required int HostUserId { get; set; }
    public required int OpponentUserId { get; set; }
}