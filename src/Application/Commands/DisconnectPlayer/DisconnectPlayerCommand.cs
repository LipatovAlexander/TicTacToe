using Application.Common.Interfaces;

namespace Application.Commands.DisconnectPlayer;

public sealed class DisconnectPlayerCommand : ICommand<DisconnectPlayerCommandResult>
{
    public required int UserId { get; set; }
}