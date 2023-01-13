namespace Application.Common.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(ApplicationUser user);
}