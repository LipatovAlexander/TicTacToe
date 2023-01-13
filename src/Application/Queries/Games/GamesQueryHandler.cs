using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Games;

public sealed class GamesQueryHandler : QueryHandlerBase<GamesQuery, GamesQueryResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GamesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Result<GamesQueryResult>> Handle(GamesQuery request,
        CancellationToken cancellationToken)
    {
        var games = await _dbContext.Games
            .Where(g => g.State == GameState.NotStarted)
            .OrderByDescending(g => g.CreatedAt)
            .Include(g => g.Host.User)
            .ToListAsync(cancellationToken: cancellationToken);

        var gamesDto = games.Select(g => new GameDto
            {
                Id = g.Id,
                CreatedAt = g.CreatedAt,
                HostUsername = g.Host.User.UserName!
            })
            .ToList();

        return Result.Success(new GamesQueryResult
        {
            Games = gamesDto
        });
    }
}