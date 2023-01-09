using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;
using WebApi.Common.Models;

namespace WebApi.Common.Mappers;

[Mapper]
public static partial class GameMapper
{
    public static partial GameDto MapToDto(this Game game);

    private static PlayerMarkDto?[,] MapToDto(this Cell[,] cells)
    {
        var result = new PlayerMarkDto?[3, 3];

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                result[x, y] = cells[x, y].Value?.MapToDto();
            }
        }

        return result;
    }

    private static partial PlayerMarkDto MapToDto(this PlayerMark mark);
}