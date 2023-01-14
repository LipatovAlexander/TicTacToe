using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.ConnectPlayer;

public sealed class ConnectPlayerCommandHandler : CommandHandlerBase<ConnectPlayerCommand, ConnectPlayerCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IClientsNotificator _clientsNotificator;

    public ConnectPlayerCommandHandler(IApplicationDbContext dbContext, IClientsNotificator clientsNotificator)
    {
        _dbContext = dbContext;
        _clientsNotificator = clientsNotificator;
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
            return Result.Success(new ConnectPlayerCommandResult());
        }

        game.State = GameState.InProgress;
        await _dbContext.SaveChangesAsync(ct);

        await _clientsNotificator.NotifyGameStartedAsync(game);

        return Result.Success(new ConnectPlayerCommandResult());
    }
}