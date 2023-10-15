using Microsoft.Extensions.Configuration;

namespace GhostRunnerStaffProfile;

public class BotConfig
{
    private readonly IConfiguration _configuration;

    public BotConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ConfigExample => _configuration.GetValue<string>(nameof(ConfigExample)) ?? "Default Setting";
    public string DbLocation => _configuration.GetValue<string>(nameof(DbLocation)) ?? $"";
}