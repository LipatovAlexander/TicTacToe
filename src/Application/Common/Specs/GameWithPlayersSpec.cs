using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specs;

public sealed class GameWithPlayersSpec : Specification<Game>
{
    public GameWithPlayersSpec(int id)
    {
        Query
            .Where(game => game.Id == id)
            .Include(game => game.Player1)
            .Include(game => game.Player2);
    }
}