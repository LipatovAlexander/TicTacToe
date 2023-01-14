using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Move;

public sealed class MoveCommandHandler : CommandHandlerBase<MoveCommand, MoveCommandResult>
{
    private readonly IApplicationDbContext _dbContext;

    public MoveCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Result<MoveCommandResult>> Handle(MoveCommand command, CancellationToken ct)
    {
        var game = await _dbContext.Games
            .Include(g => g.Host.User)
            .Include(g => g.Opponent!.User)
            .FirstOrDefaultAsync(
                g => (g.Host.UserId == command.UserId || g.Opponent!.UserId == command.UserId)
                    && g.State == GameState.InProgress, ct);

        if (game is null)
        {
            return Result.Failure<MoveCommandResult>("Game not found");
        }

        var player = game.Host.UserId == command.UserId ? game.Host : game.Opponent!;

        if (!game.IsPlayerTurn(player.Mark))
        {
            return Result.Failure<MoveCommandResult>("It's not your turn");
        }

        if (!game.Board.InBound(command.X, command.Y))
        {
            return Result.Failure<MoveCommandResult>("You're out of bounds");
        }

        if (game.Board.GetMark(command.X, command.Y) is not null)
        {
            return Result.Failure<MoveCommandResult>("Cell is not empty");
        }
        
        game.Move(command.X, command.Y, player.Mark);
        await _dbContext.SaveChangesAsync(ct);

        return Result.Success(new MoveCommandResult
        {
            X = command.X,
            Y = command.Y,
            Mark = player.Mark,
            State = game.State,
            HostUserId = game.Host.UserId,
            OpponentUserId = game.Opponent!.UserId
        });
    }
}