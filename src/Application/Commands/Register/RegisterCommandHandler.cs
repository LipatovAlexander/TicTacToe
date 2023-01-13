using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Register;

public sealed class RegisterCommandHandler : CommandHandlerBase<RegisterCommand, RegisterCommandResult>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task<Result<RegisterCommandResult>> Handle(RegisterCommand command, CancellationToken ct)
    {
        var user = new ApplicationUser
        {
            UserName = command.Username
        };

        var identityResult = await _userManager.CreateAsync(user, command.Password);

        if (identityResult.Succeeded)
        {
            return Result.Success(new RegisterCommandResult());
        }

        var errors = identityResult.Errors.Select(e => e.Description).ToArray();
        return Result.Failure<RegisterCommandResult>(errors);
    }
}