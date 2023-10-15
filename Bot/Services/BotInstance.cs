using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GhostRunnerStaffProfile.Services;

public class BotInstance : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<BotInstance> _logger;

    public BotInstance(DiscordSocketClient client, IConfiguration configuration,ILogger<BotInstance> logger)
    {
        _client = client;
        _configuration = configuration;
        _logger = logger;
        client.Log += ClientOnLog;
    }

    private Task ClientOnLog(LogMessage arg)
    {
        switch (arg.Severity)
        {
            case LogSeverity.Critical:
                _logger.LogCritical(arg.Exception,arg.Message);
                break;
            case LogSeverity.Error:
                _logger.LogError(arg.Exception,arg.Message);
                break;
            case LogSeverity.Warning:
                _logger.LogWarning(arg.Exception,arg.Message);
                break;
            case LogSeverity.Info:
                _logger.LogInformation(arg.Exception,arg.Message);
                break;
            case LogSeverity.Verbose:
                _logger.LogTrace(arg.Exception,arg.Message);
                break;
            case LogSeverity.Debug:
                _logger.LogDebug(arg.Exception,arg.Message);
                break;
        }
    
        return Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.LoginAsync(TokenType.Bot, _configuration["Token"]);
        await _client.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.LogoutAsync();
        await _client.StopAsync();
    }
}