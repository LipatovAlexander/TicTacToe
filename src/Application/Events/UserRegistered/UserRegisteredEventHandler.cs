using Microsoft.Extensions.Logging;

namespace Application.Events.UserRegistered;

public sealed class UserRegisteredEventHandler : EventHandlerBase<UserRegisteredEvent>
{
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger)
    {
        _logger = logger;
    }

    protected override Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User with id: {UserId} and username: {UserName} has been registered", @event.UserId, @event.UserName);
        return Task.CompletedTask;
    }
}