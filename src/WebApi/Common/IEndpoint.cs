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