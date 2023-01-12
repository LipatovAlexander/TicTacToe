namespace Domain.Entities;

public sealed class Board
{
    public required int Size { get; set; }
    public required Cell[,] Cells { get; set; }
}