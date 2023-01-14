using Microsoft.Extensions.Logging;

namespace Application.Events.GameStarted;

public sealed class GameStartedEventHandler : EventHandlerBase<GameStartedEvent>
{
    private readonly ILogger<GameStartedEventHandler> _logger;

    public GameStartedEventHandler(ILogger<GameStartedEventHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task Handle(GameStartedEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Game with id {GameId} has been started for host {HostUserId} and opponent {OpponentUserId}",
            @event.GameId, @event.HostUserId, @event.OpponentUserId);
    }
}