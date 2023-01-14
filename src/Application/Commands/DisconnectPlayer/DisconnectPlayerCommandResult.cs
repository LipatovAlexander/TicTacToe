using Domain.Enums;

namespace Application.Commands.DisconnectPlayer;

public sealed class DisconnectPlayerCommandResult
{
    public required bool OpponentWon { get; set; }
    public int OpponentUserId { get; set; }
    public GameState State { get; set; }
}