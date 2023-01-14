using Application.Common.Interfaces;

namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommand : ICommand<ConnectPlayerCommandResult>
{
    public required int UserId { get; set; }
}