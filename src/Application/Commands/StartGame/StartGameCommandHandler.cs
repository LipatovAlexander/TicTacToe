using MassTransit;

namespace Application.Commands.StartGame;

public sealed class StartGameCommandHandler : IConsumer<StartGameCommand>
{
    public async Task Consume(ConsumeContext<StartGameCommand> context)
    {
        throw new NotImplementedException();
    }
}