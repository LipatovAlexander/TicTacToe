using Application.Common;
using Application.Events.Test;
using Application.Queries.Test;

namespace Application.Commands.Test;

public sealed class TestCommandHandler : CommandHandlerBase<TestCommand, TestCommandResult>
{
    private readonly IApplicationMediator _mediator;

    public TestCommandHandler(IApplicationMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<TestCommandResult> Handle(TestCommand command)
    {
        Console.WriteLine("Test command handler");
    
        var query = new TestQuery();

        var result = await _mediator.Query<TestQuery, TestQueryResult>(query);

        var @event = new TestEvent();
        await _mediator.Event(@event);
        
        return new TestCommandResult
        {
            Value = result.Value
        };
    }
}