using Domain.Enums;

namespace Domain.Entities;

public sealed class Cell
{
    public required PlayerMark? Value { get; set; }
    
    public bool IsEmpty => Value is null;
}