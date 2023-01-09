namespace WebApi.Common.Models;

public sealed class GameDto
{
    public int Id { get; set; }
    public PlayerDto Player1 { get; set; } = default!;
    public PlayerDto Player2 { get; set; } = default!;
    public BoardDto Board { get; set; } = default!;
    public GameStateDto State { get; set; }
}