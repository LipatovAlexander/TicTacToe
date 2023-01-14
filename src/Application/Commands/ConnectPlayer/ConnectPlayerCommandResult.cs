namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommandResult
{
    public required bool GameStarted { get; set; }
    public int HostUserId { get; set; }
    public string HostUsername { get; set; } = default!;
    public int OpponentUserId { get; set; }
    public string OpponentUsername { get; set; } = default!;
}