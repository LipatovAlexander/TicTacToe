namespace WebApi.Common.Models;

public sealed class PlayerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserNickname { get; set; } = default!;
    public PlayerMarkDto Mark { get; set; }
}