using Application.Common.Interfaces;
using Application.Events.GameOver;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.DisconnectPlayer;

public sealed class
    DisconnectPlayerCommandHandler : CommandHandlerBase<DisconnectPlayerCommand, DisconnectPlayerCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IApplicationMediator _applicationMediator;

    public DisconnectPlayerCommandHandler(IApplicationDbContext dbContext, IApplicationMediator applicationMediator)
    {
        _dbContext = dbContext;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<Result<DisconnectPlayerCommandResult>> Handle(DisconnectPlayerCommand command,
        CancellationToken ct)
    {
        var game = await _dbContext.Games
            .Include(g => g.Host.User)
            .Include(g => g.Opponent!.User)
            .FirstOrDefaultAsync(
                g => (g.Host.UserId == command.UserId || g.Opponent!.UserId == command.UserId)
                    && (g.State == GameState.NotStarted || g.State == GameState.InProgress), ct);

        if (game is null)
        {
            return Result.Failure<DisconnectPlayerCommandResult>("Game not found");
        }

        if (game.State is GameState.NotStarted)
        {
            game.State = GameState.Draw;
            await _dbContext.SaveChangesAsync(ct);

            await RaiseGameOver(game, ct);
            
            return Result.Success(new DisconnectPlayerCommandResult
            {
                OpponentWon = false
            });
        }

        if (game.State is not GameState.InProgress)
        {
            return Result.Success(new DisconnectPlayerCommandResult
            {
                OpponentWon = false
            });
        }

        var winner = game.Host.UserId == command.UserId ? game.Opponent : game.Host;
        game.State = winner!.Mark is PlayerMark.Crosses ? GameState.CrossesWon : GameState.NoughtsWon;

        await _dbContext.SaveChangesAsync(ct);

        await RaiseGameOver(game, ct);

        return Result.Success(new DisconnectPlayerCommandResult
        {
            OpponentWon = true,
            OpponentUserId = winner.UserId,
            State = game.State
        });
    }

    private async Task RaiseGameOver(Game game, CancellationToken ct)
    {
        var gameOver = new GameOverEvent
        {
            GameId = game.Id,
            GameState = game.State
        };

        await _applicationMediator.Event(gameOver, ct);
    }
}