﻿using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.DisconnectPlayer;

public sealed class
    DisconnectPlayerCommandHandler : CommandHandlerBase<DisconnectPlayerCommand, DisconnectPlayerCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IClientsNotificator _clientsNotificator;

    public DisconnectPlayerCommandHandler(IApplicationDbContext dbContext, IClientsNotificator clientsNotificator)
    {
        _dbContext = dbContext;
        _clientsNotificator = clientsNotificator;
    }

    protected override async Task<Result<DisconnectPlayerCommandResult>> Handle(DisconnectPlayerCommand command,
        CancellationToken ct)
    {
        var game = await _dbContext.Games
            .Include(g => g.Host.User)
            .Include(g => g.Opponent!.User)
            .FirstOrDefaultAsync(
                g => g.Host.UserId == command.UserId || g.Opponent!.UserId == command.UserId, ct);

        if (game is null)
        {
            return Result.Success(new DisconnectPlayerCommandResult());
        }

        if (game.State is GameState.NotStarted)
        {
            game.State = GameState.Draw;
            await _dbContext.SaveChangesAsync(ct);
            return Result.Success(new DisconnectPlayerCommandResult());
        }

        if (game.State is not GameState.InProgress)
        {
            return Result.Success(new DisconnectPlayerCommandResult());
        }

        var winner = game.Host.UserId == command.UserId ? game.Opponent : game.Host;
        game.State = winner!.Mark is PlayerMark.Crosses ? GameState.CrossesWon : GameState.NoughtsWon;

        await _dbContext.SaveChangesAsync(ct);
        await _clientsNotificator.NotifyOpponentDisconnected(winner, game.State);
        return Result.Success(new DisconnectPlayerCommandResult());
    }
}