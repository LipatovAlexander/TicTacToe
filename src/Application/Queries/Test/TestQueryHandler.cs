using Application.Common;

namespace Application.Queries.Test;

public sealed class TestQueryHandler : QueryHandlerBase<TestQuery, TestQueryResult>
{
    protected override async Task<TestQueryResult> Handle(TestQuery request)
    {
        Console.WriteLine("Test query handler");

        return new TestQueryResult
        {
            Value = 69
        };
    }
}