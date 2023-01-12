using Application;
using Application.Commands.Test;
using WebApi.Common;

namespace WebApi.Endpoints.Test;

public sealed class TestEndpoint : IEndpoint<TestRequest, TestResponse>
{
    private readonly IApplicationMediator _mediator;

    public TestEndpoint(IApplicationMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TestResponse> HandleAsync([AsParameters] TestRequest request)
    {
        var command = new TestCommand();

        var result = await _mediator.Command<TestCommand, TestCommandResult>(command);

        return new TestResponse
        {
            Value = result.Value
        };
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/random", HandleAsync);
    }
}