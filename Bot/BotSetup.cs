using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using GhostRunnerStaffProfile.Data;
using GhostRunnerStaffProfile.Data.Models;
using GhostRunnerStaffProfile.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GhostRunnerStaffProfile;

public static class BotSetup
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.GuildIntegrations | GatewayIntents.Guilds
        };
        services.AddDbContext<Db>();
        services.AddSingleton(new DiscordSocketClient(config));
        services.AddSingleton<InteractionService>();
        services.AddSingleton<BotConfig>();
        services.AddHostedService<BotInstance>();
        services.AddHostedService<InteractionHandler>();
        services.AddTransient<UserNoteService>();

        return services;
    }
}