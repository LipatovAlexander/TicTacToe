using Application.Common.Interfaces;

namespace Application.Commands.Register;

public sealed class RegisterCommand : ICommand<RegisterCommandResult>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}