using Infrastructure.Common;

namespace Infrastructure.Configurations.Settings;

public sealed class RabbitSettings : ISettings
{
    public static string SectionName => "Rabbit";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}