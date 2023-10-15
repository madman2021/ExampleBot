using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace GhostRunnerStaffProfile.Services;

public class InteractionHandler : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _serviceProvider;

    public InteractionHandler(DiscordSocketClient client,InteractionService interactions,IServiceProvider serviceProvider)
    {
        _client = client;
        _interactions = interactions;
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
        _client.Ready += async () =>
        {
            var commands = await _interactions.RegisterCommandsGloballyAsync(true);
        };
        _client.InteractionCreated += ClientOnInteractionCreated;
    }

    private async Task ClientOnInteractionCreated(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);
            await _interactions.ExecuteCommandAsync(context, _serviceProvider);
        }
        catch
        {
            if (interaction.Type == InteractionType.ApplicationCommand)
            {
                await interaction.GetOriginalResponseAsync()
                    .ContinueWith(msg => msg.Result.DeleteAsync());
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _interactions.Dispose();
        return Task.CompletedTask;
    }
}