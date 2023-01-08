namespace Domain.Entities;

public sealed class Board : BaseEntity
{
    public ICollection<Cell> Cells { get; set; } = default!;
}