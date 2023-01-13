using Application.Common.Interfaces;
using Application.Extensions;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.CurrentGame;

public sealed class CurrentGameQueryHandler : QueryHandlerBase<CurrentGameQuery, CurrentGameQueryResult>
{
    private readonly IApplicationDbContext _dbContext;

    public CurrentGameQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Result<CurrentGameQueryResult>> Handle(CurrentGameQuery request,
        CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.Host.User)
            .Include(g => g.Opponent!.User)
            .FirstOrDefaultAsync(g =>
                    (g.State == GameState.NotStarted || g.State == GameState.InProgress) &&
                    (g.Host.UserId == request.UserId
                        || g.Opponent != null && g.Opponent.UserId == request.UserId),
                cancellationToken);

        if (game is null)
        {
            return Result.Failure<CurrentGameQueryResult>("You have no active games");
        }

        var isHost = game.Host.UserId == request.UserId;

        return Result.Success(new CurrentGameQueryResult
        {
            State = game.State,
            Mark = isHost ? game.Host.Mark : game.Opponent!.Mark,
            OpponentUsername = (isHost ? game.Opponent?.User.UserName : game.Host.User.UserName)!,
            Board = game.Board.Cells.ToJagged().Select(c => c.Select(cc => cc.PlayerMark).ToArray()).ToArray()
        });
    }
}