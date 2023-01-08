namespace Domain.Entities;

public sealed class Player : BaseEntity
{
    public required PlayerMark Mark { get; set; }
    
    public required string Nickname { get; set; }

    public ICollection<Game> Games { get; set; } = default!;
}

public enum PlayerMark
{
    Nought,
    Cross
}