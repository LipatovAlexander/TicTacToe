using Application.Common.Interfaces;
using Application.Events.GameCreated;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CreateGame;

public sealed class CreateGameCommandHandler : CommandHandlerBase<CreateGameCommand, CreateGameCommandResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IRandomizer _randomizer;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IApplicationMediator _applicationMediator;

    public CreateGameCommandHandler(IApplicationDbContext dbContext, IRandomizer randomizer,
        IDateTimeProvider dateTimeProvider, IApplicationMediator applicationMediator)
    {
        _dbContext = dbContext;
        _randomizer = randomizer;
        _dateTimeProvider = dateTimeProvider;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<Result<CreateGameCommandResult>> Handle(CreateGameCommand command,
        CancellationToken ct)
    {
        var hostUserId = command.UserId;

        if (await UserAlreadyPlayingAsync(hostUserId, ct))
        {
            return Result.Failure<CreateGameCommandResult>("You are already playing");
        }

        var game = new Game
        {
            Host = new Player
            {
                UserId = hostUserId,
                Mark = _randomizer.EnumValue<PlayerMark>()
            },
            Board = new Board(3),
            State = GameState.NotStarted,
            CreatedAt = _dateTimeProvider.UtcNow
        };

        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync(ct);

        var gameCreated = new GameCreatedEvent
        {
            GameId = game.Id,
            HostUserId = hostUserId,
            CreatedAt = game.CreatedAt
        };

        await _applicationMediator.Event(gameCreated, ct);

        return Result.Success(new CreateGameCommandResult
        {
            GameId = game.Id
        });
    }

    private async Task<bool> UserAlreadyPlayingAsync(int userId, CancellationToken ct)
    {
        return await _dbContext.Games.AnyAsync(game =>
            (game.State == GameState.NotStarted || game.State == GameState.InProgress)
            && (game.Host.UserId == userId || game.Opponent != null && game.Opponent.UserId == userId), ct);
    }
}