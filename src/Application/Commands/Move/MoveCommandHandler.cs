using MassTransit;

namespace Application.Commands.Move;

public sealed class MoveCommandHandler : IConsumer<MoveCommand>
{
    public async Task Consume(ConsumeContext<MoveCommand> context)
    {
        throw new NotImplementedException();
    }
}