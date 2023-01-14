using Application.Events.UserRegistered;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Register;

public sealed class RegisterCommandHandler : CommandHandlerBase<RegisterCommand, RegisterCommandResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationMediator _applicationMediator;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IApplicationMediator applicationMediator)
    {
        _userManager = userManager;
        _applicationMediator = applicationMediator;
    }

    protected override async Task<Result<RegisterCommandResult>> Handle(RegisterCommand command, CancellationToken ct)
    {
        var user = new ApplicationUser
        {
            UserName = command.Username
        };

        var identityResult = await _userManager.CreateAsync(user, command.Password);

        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors.Select(e => e.Description).ToArray();
            return Result.Failure<RegisterCommandResult>(errors);
        }

        var userRegistered = new UserRegisteredEvent
        {
            UserId = user.Id,
            UserName = user.UserName
        };

        await _applicationMediator.Event(userRegistered, ct);
        return Result.Success(new RegisterCommandResult());
    }
}