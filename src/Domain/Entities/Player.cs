using Domain.Enums;

namespace Domain.Entities;

public sealed class Player : BaseEntity
{
    public required int UserId { get; set; }
    public required PlayerMark Mark { get; set; }
}