using Domain.Entities;

namespace Application.Commands.FindOpponent;

public sealed class FindOpponentCommand
{
    public required int UserId { get; set; }

    public User User { get; set; } = default!;
}