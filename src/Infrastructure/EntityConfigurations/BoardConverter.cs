using System.Diagnostics;
using System.Text;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityConfigurations;

public sealed class BoardConverter : ValueConverter<Board, string>
{
    public BoardConverter() : base(board => BoardToString(board), str => StringToBoard(str))
    {
    }

    public static string BoardToString(Board board)
    {
        var builder = new StringBuilder();
        builder.Append('[');

        for (var y = 0; y < board.Size; y++)
        {
            for (var x = 0; x < board.Size; x++)
            {
                var cell = board.Cells[x, y];
                
                var character = cell.PlayerMark switch
                {
                    PlayerMark.Crosses => "X",
                    PlayerMark.Noughts => "O",
                    null => "_",
                    _ => throw new UnreachableException()
                };
                
                builder.Append(character);
            }
        }

        builder.Append(']');
        return builder.ToString();
    }

    public static Board StringToBoard(string str)
    {
        var chars = str.Trim('[', ']').ToCharArray();
        var boardSize = (int)Math.Sqrt(chars.Length);

        var board = new Board
        {
            Size = boardSize,
            Cells = new Cell[boardSize, boardSize]
        };

        var arr = chars.Chunk(boardSize).ToArray();

        for (var y = 0; y < boardSize; y++)
        {
            for (var x = 0; x < boardSize; x++)
            {
                var mark = arr[y][x] switch
                {
                    'X' => PlayerMark.Crosses,
                    'O' => PlayerMark.Noughts,
                    '_' => null as PlayerMark?,
                     _ => throw new UnreachableException()
                };

                board.Cells[x, y] = new Cell
                {
                    PlayerMark = mark
                };
            }
        }

        return board;
    }
}