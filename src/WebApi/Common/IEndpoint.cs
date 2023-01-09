﻿namespace WebApi.Common;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}

public interface IEndpoint<TResult> : IEndpoint
{
    Task<TResult> HandleAsync();
}

public interface IEndpoint<TResult, in TRequest> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest request);
}

public interface IEndpoint<TResult, in TRequest1, in TRequest2> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest1 request1, TRequest2 request2);
}

public interface IEndpoint<TResult, in TRequest1, in TRequest2, in TRequest3> : IEndpoint
{
    Task<TResult> HandleAsync(TRequest1 request1, TRequest2 request2, TRequest3 request3);
}