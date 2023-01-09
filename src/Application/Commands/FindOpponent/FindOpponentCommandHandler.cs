using MassTransit;

namespace Application.Commands.FindOpponent;

public sealed class FindOpponentCommandHandler : IConsumer<FindOpponentCommand>
{
    public async Task Consume(ConsumeContext<FindOpponentCommand> context)
    {
        throw new NotImplementedException();
    }
}