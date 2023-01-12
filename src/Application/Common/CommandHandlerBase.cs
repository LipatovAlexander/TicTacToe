using Application.Common.Interfaces;
using MassTransit;

namespace Application.Common;

public abstract class CommandHandlerBase<TCommand, TResult> : IConsumer<TCommand>
    where TCommand : class, ICommand<TResult>
{
    protected abstract Task<TResult> Handle(TCommand command, CancellationToken ct);
    
    public async Task Consume(ConsumeContext<TCommand> context)
    {
        var result = await Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(result!);
    }
}