namespace Domain.Entities;

public sealed class Board
{
    public Board(int size)
    {
        Size = size;
        Cells = new Cell[size, size];

        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                Cells[x, y] = new Cell();
            }
        }
    }

    public int Size { get; }
    public Cell[,] Cells { get; }
}