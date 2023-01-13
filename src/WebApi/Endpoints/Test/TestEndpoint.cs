using System.Security.Claims;
using WebApi.Common;

namespace WebApi.Endpoints.Test;

public sealed class TestEndpoint : IEndpoint<HttpContext, TestResponse>
{
    public async Task<Response<TestResponse>> HandleAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        return new Response<TestResponse>(new TestResponse
        {
            Username = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!
        });
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", HandleAsync);
    }
}