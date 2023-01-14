using Microsoft.Extensions.Logging;

namespace Application.Events.GameOver;

public sealed class GameOverEventHandler : EventHandlerBase<GameOverEvent>
{
    private readonly ILogger<GameOverEventHandler> _logger;

    public GameOverEventHandler(ILogger<GameOverEventHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task Handle(GameOverEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Game with id {GameId} has been completed with state {GameState}",
            @event.GameId, @event.GameState);
    }
}