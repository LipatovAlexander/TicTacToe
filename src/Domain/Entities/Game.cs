using Domain.Enums;

namespace Domain.Entities;

public sealed class Game : BaseEntity
{
    public Player Host { get; set; } = default!;
    public Player? Opponent { get; set; }
    public required GameState State { get; set; }
    public required Board Board { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }

    public void Move(int x, int y, PlayerMark mark)
    {
        if (State is not GameState.InProgress)
        {
            throw new InvalidOperationException();
        }

        if (!Board.InBound(x, y))
        {
            throw new InvalidOperationException();
        }

        if (!IsPlayerTurn(mark))
        {
            throw new InvalidOperationException();
        }

        if (Board.GetMark(x, y) is not null)
        {
            throw new InvalidOperationException();
        }

        Board.SetMark(x, y, mark);
        UpdateState();
    }

    public void UpdateState()
    {
        if (Board.IsFilled())
        {
            State = GameState.Draw;
            return;
        }

        if (CheckWinner(out var mark))
        {
            State = mark is PlayerMark.Crosses ? GameState.CrossesWon : GameState.NoughtsWon;
            return;
        }

        State = GameState.InProgress;
    }

    public bool IsPlayerTurn(PlayerMark mark)
    {
        var crossesCount = Board.Count(c => c.PlayerMark is PlayerMark.Crosses);
        var noughtsCount = Board.Count(c => c.PlayerMark is PlayerMark.Noughts);

        var nextMark = crossesCount == noughtsCount ? PlayerMark.Crosses : PlayerMark.Noughts;
        return nextMark == mark;
    }

    private bool CheckWinner(out PlayerMark mark)
    {
        var lines = Board.GetLines();

        foreach (var line in lines)
        {
            var t = line[0].PlayerMark;

            if (!t.HasValue || line.Any(c => c.PlayerMark != t))
            {
                continue;
            }

            mark = t.Value;
            return true;
        }

        mark = default;
        return false;
    }
}