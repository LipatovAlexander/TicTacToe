namespace Application.Commands.FindOpponent;

public static class FindOpponentCommandResult
{
    public sealed class Found
    {
        public required int OpponentId { get; set; }
    }
    
    public sealed class Awaiting
    {
    }
}