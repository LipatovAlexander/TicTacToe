using Infrastructure.Common;

namespace Infrastructure.Configurations.Settings;

public sealed class BusSettings : ISettings
{
    public static string SectionName => "Bus";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}