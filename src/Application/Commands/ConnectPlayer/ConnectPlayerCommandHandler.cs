using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommandHandler : CommandHandlerBase<ConnectPlayerCommand, ConnectPlayerCommandResult>
{
    private readonly IApplicationDbContext _dbContext;

    public ConnectPlayerCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Result<ConnectPlayerCommandResult>> Handle(ConnectPlayerCommand command,
        CancellationToken ct)
    {
        var game = await _dbContext.Games
            .Include(g => g.Host.User)
            .Include(g => g.Opponent!.User)
            .FirstOrDefaultAsync(
                g => g.Host.UserId == command.UserId || g.Opponent!.UserId == command.UserId, ct);

        if (game?.State is not GameState.NotStarted || game.Opponent is null)
        {
            return Result.Success(new ConnectPlayerCommandResult
            {
                GameStarted = false
            });
        }

        game.State = GameState.InProgress;
        await _dbContext.SaveChangesAsync(ct);

        return Result.Success(new ConnectPlayerCommandResult
        {
            GameStarted = true,
            HostUserId = game.Host.UserId,
            HostUsername = game.Host.User.UserName!,
            OpponentUserId = game.Opponent.UserId,
            OpponentUsername = game.Opponent.User.UserName!
        });
    }
}