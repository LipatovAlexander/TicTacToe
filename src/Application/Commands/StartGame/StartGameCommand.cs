using Domain.Entities;

namespace Application.Commands.StartGame;

public sealed class StartGameCommand
{
    public required int User1Id { get; set; }
    public required int User2Id { get; set; }

    public User User1 { get; set; }
    public User User2 { get; set; }
}