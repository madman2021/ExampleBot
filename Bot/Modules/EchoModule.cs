using Discord;
using Discord.Interactions;

namespace GhostRunnerStaffProfile.Modules;

public class EchoModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("echo","bot will echo input")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public async Task Echo(string text)=>await RespondAsync(text);
}