using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using GhostRunnerStaffProfile.Services;

namespace GhostRunnerStaffProfile.Modules;

public class UserNoteModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly UserNoteService _userNoteService;

    public UserNoteModule(UserNoteService userNoteService)
    {
        _userNoteService = userNoteService;
    }
    [SlashCommand("usernote", "Get saved note for user")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public async Task UserNote(SocketGuildUser user)
    {
        var un = await _userNoteService.GetUserNote(user.Id);
        if (un == null)
        {
            await RespondAsync("User doesn't have a note");
        }
        else
        {
            await RespondAsync("User note = " + un.Note);
        }
    }
    [SlashCommand("set-user-note", "Get saved note for user")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public async Task SetUserNote(SocketGuildUser user,string note)
    {
        if (note.Length >= 50)
        {
            await RespondAsync("Note must be less than 50 character");
            return;
        }
        await _userNoteService.UpdateNote(user.Id,note);
        await RespondAsync("Note set");
    }
}