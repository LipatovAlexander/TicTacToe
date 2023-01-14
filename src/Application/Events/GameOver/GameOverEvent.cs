using Application.Common.Interfaces;
using Domain.Enums;

namespace Application.Events.GameOver;

public sealed class GameOverEvent : IEvent
{
    public required int GameId { get; set; }
    public required GameState GameState { get; set; }
}