using Domain.Enums;

namespace Domain.Entities;

public sealed class Player : BaseEntity
{
    public required User User { get; set; }

    public required PlayerMark Mark { get; set; }

    public ICollection<Game> Games { get; set; } = default!;
}