using Application.Common;

namespace Application.Events.Test;

public sealed class TestEventHandler : EventHandlerBase<TestEvent>
{
    protected override async Task Handle(TestEvent @event)
    {
        Console.WriteLine("Test event handler");
    }
}