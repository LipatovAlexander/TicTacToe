using Application.Common.Interfaces;
using Application.Events.GameOver;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Move;

public sealed class MoveCommandHandler : CommandHandlerBase<MoveCommand, MoveCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IApplicationMediator _applicationMediator;

    public MoveCommandHandler(IApplicationDbContext dbContext, IApplicationMediator applicationMediator)
    {
        _dbContext = dbContext;
        _applicationMediator = applicationMediator;
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
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync(ct);

        if (game.State is not GameState.InProgress)
        {
            var gameOver = new GameOverEvent
            {
                GameId = game.Id,
                GameState = game.State
            };

            await _applicationMediator.Event(gameOver, ct);
        }

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