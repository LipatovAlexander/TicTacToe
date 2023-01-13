using Application.Common.Interfaces;
using MassTransit;

namespace Application.Common;

public abstract class QueryHandlerBase<TQuery, TResult> : IConsumer<TQuery>
    where TQuery : class, IQuery<TResult>
{
    protected abstract Task<Result<TResult>> Handle(TQuery request, CancellationToken cancellationToken);
    
    public async Task Consume(ConsumeContext<TQuery> context)
    {
        var result = await Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(result!);
    }
}