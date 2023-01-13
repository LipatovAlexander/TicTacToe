using Application;
using Application.Commands.Login;
using WebApi.Common;

namespace WebApi.Endpoints.Login;

public sealed class LoginEndpoint : IEndpoint<LoginRequest, LoginResponse>
{
    private readonly IApplicationMediator _applicationMediator;

    public LoginEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await _applicationMediator.Command<LoginCommand, LoginCommandResult>(command, cancellationToken);

        if (result.IsSuccessful)
        {
            return Response.Success(new LoginResponse
            {
                Jwt = result.Data.Token
            });
        }

        return Response.Failure<LoginResponse>(result.Errors);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", HandleAsync)
            .AllowAnonymous();
    }
}