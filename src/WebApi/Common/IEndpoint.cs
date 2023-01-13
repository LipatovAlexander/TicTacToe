namespace WebApi.Common;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}

public interface IEndpoint<TResponse> : IEndpoint
{
    Task<Response<TResponse>> HandleAsync(CancellationToken cancellationToken);
}

public interface IEndpoint<in TRequest, TResponse> : IEndpoint
{
    Task<Response<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

public interface IEndpoint<in TRequest1, in TRequest2, TResponse> : IEndpoint
{
    Task<Response<TResponse>> HandleAsync(TRequest1 request1, TRequest2 request2, CancellationToken cancellationToken);
}