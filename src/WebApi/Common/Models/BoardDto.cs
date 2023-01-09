namespace WebApi.Common.Models;

public sealed class BoardDto
{
    public PlayerMarkDto?[,] Cells { get; set; } = default!;
}