using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.JoinGame;

public sealed class JoinGameCommandHandler : CommandHandlerBase<JoinGameCommand, JoinGameCommandResult>
{
    private readonly IApplicationDbContext _dbContext;

    public JoinGameCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<Result<JoinGameCommandResult>> Handle(JoinGameCommand command, CancellationToken ct)
    {
        if (await UserAlreadyPlayingAsync(command.UserId, ct))
        {
            return Result.Failure<JoinGameCommandResult>("You are already playing");
        }

        var game = await _dbContext.Games
            .Include(g => g.Host)
            .FirstOrDefaultAsync(g => g.Id == command.GameId, ct);

        if (game is null)
        {
            return Result.Failure<JoinGameCommandResult>("Game not found");
        }

        if (game.State is not GameState.NotStarted)
        {
            return Result.Failure<JoinGameCommandResult>("The game has already started");
        }

        game.Opponent = new Player
        {
            UserId = command.UserId,
            Mark = game.Host.Mark is PlayerMark.Crosses ? PlayerMark.Noughts : PlayerMark.Crosses
        };

        await _dbContext.SaveChangesAsync(ct);
        
        return Result.Success(new JoinGameCommandResult());
    }
    
    private async Task<bool> UserAlreadyPlayingAsync(int userId, CancellationToken ct)
    {
        return await _dbContext.Games.AnyAsync(game =>
            (game.State == GameState.NotStarted || game.State == GameState.InProgress)
            && (game.Host.UserId == userId || game.Opponent != null && game.Opponent.UserId == userId), ct);
    }
}