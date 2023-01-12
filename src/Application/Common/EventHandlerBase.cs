using Application.Common.Interfaces;
using MassTransit;

namespace Application.Common;

public abstract class EventHandlerBase<TEvent> : IConsumer<TEvent>
    where TEvent : class, IEvent
{
    protected abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
    
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        await Handle(context.Message, context.CancellationToken);
    }
}