using Domain.Enums;

namespace Domain.Entities;

public sealed class Board
{
    public required Cell[,] Cells { get; set; } = default!;

    public bool IsEmpty(int x, int y)
    {
        return Cells[x, y].IsEmpty;
    }

    public void Mark(int x, int y, PlayerMark playerMark)
    {
        Cells[x, y].Value = playerMark;
    }
}