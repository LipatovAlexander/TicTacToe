namespace WebApi.Endpoints.Login;

public sealed class LoginResponse
{
    public required string Jwt { get; set; }
}