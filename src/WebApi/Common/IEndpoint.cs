namespace WebApi.Common;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}

public interface IEndpoint<TResult> : IEndpoint
{
    Task<TResult> HandleAsync();
}

public interface IEndpoint<in TRequest, TResult> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest request);
}

public interface IEndpoint<in TRequest1, in TRequest2, TResult> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest1 request1, TRequest2 request2);
}

public interface IEndpoint<in TRequest1, in TRequest2, in TRequest3, TResult> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest1 request1, TRequest2 request2, TRequest3 request3);
}