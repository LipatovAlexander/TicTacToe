using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Login;

public sealed class LoginCommandHandler : CommandHandlerBase<LoginCommand, LoginCommandResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }

    protected override async Task<Result<LoginCommandResult>> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(command.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, command.Password))
        {
            return Result.Failure<LoginCommandResult>(new [] { "Incorrect username or password" });
        }

        var token = _jwtGenerator.GenerateToken(user);
        var result = new LoginCommandResult
        {
            Token = token
        };

        return Result.Success(result);
    }
}