using Microsoft.Extensions.Logging;

namespace Application.Events.GameCreated;

public sealed class GameCreatedEventHandler : EventHandlerBase<GameCreatedEvent>
{
    private readonly ILogger<GameCreatedEventHandler> _logger;

    public GameCreatedEventHandler(ILogger<GameCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task Handle(GameCreatedEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Game with id: {GameId} has been created by user {HostUserId} at {CreatedAt}",
            @event.GameId, @event.HostUserId, @event.CreatedAt);
    }
}