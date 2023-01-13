using Application;
using Application.Commands.Register;
using WebApi.Common;

namespace WebApi.Endpoints.Register;

public sealed class RegisterEndpoint : IEndpoint<RegisterRequest, RegisterResponse>
{
    private readonly IApplicationMediator _applicationMediator;

    public RegisterEndpoint(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task<Response<RegisterResponse>> HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await _applicationMediator.Command<RegisterCommand, RegisterCommandResult>(command, cancellationToken);

        return result.IsSuccessful
            ? Response.Success(new RegisterResponse())
            : Response.Failure<RegisterResponse>(result.Errors);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", HandleAsync)
            .AllowAnonymous();
    }
}