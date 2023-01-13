using Application.Common.Interfaces;

namespace Application.Commands.Login;

public sealed class LoginCommand : ICommand<LoginCommandResult>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}