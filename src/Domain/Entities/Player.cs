using Domain.Enums;

namespace Domain.Entities;

public sealed class Player : BaseEntity
{
    public User User { get; set; } = default!;
    public required PlayerMark Mark { get; set; }
}