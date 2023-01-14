using System.Collections;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Board : IEnumerable<Cell>
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

    public bool IsFilled()
    {
        return this.All(c => c.PlayerMark is not null);
    }

    public IEnumerable<Cell[]> GetLines()
    {
        for (var row = 0; row < Size; row++)
        {
            var line = new Cell[Size];
            for (var col = 0; col < Size; col++)
            {
                line[col] = Cells[col, row];
            }
            yield return line;
        }

        for (var col = 0; col < Size; col++)
        {
            var line = new Cell[Size];
            for (var row = 0; row < Size; row++)
            {
                line[row] = Cells[col, row];
            }
            yield return line;
        }

        var diagonal1 = new Cell[Size];
        for (var i = 0; i < Size; i++)
        {
            diagonal1[i] = Cells[i, i];
        }
        yield return diagonal1;

        var diagonal2 = new Cell[Size];
        for (var i = 0; i < Size; i++)
        {
            diagonal2[i] = Cells[i, Size - 1 - i];
        }
        yield return diagonal2;
    }

    public bool InBound(int x, int y)
    {
        return x >= 0 && x < Size
            && y >= 0 && y < Size;
    }

    public PlayerMark? GetMark(int x, int y)
    {
        return Cells[x, y].PlayerMark;
    }

    public void SetMark(int x, int y, PlayerMark mark)
    {
        Cells[x, y].PlayerMark = mark;
    }
    
    public IEnumerator<Cell> GetEnumerator()
    {
        return Cells.OfType<Cell>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}