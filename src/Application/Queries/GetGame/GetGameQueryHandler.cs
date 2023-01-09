using MassTransit;

namespace Application.Queries.GetGame;

public sealed class GetGameQueryHandler : IConsumer<GetGameQuery>
{
    public async Task Consume(ConsumeContext<GetGameQuery> context)
    {
        throw new NotImplementedException();
    }
}