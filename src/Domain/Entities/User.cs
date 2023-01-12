namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public required string Nickname { get; set; }
}