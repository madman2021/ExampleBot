using GhostRunnerStaffProfile.Data;
using GhostRunnerStaffProfile.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GhostRunnerStaffProfile.Services;

public class UserNoteService
{
    private readonly Db _db;

    public UserNoteService(Db db)
    {
        _db = db;
    }

    public async Task<UserNote?> GetUserNote(ulong id)
    {
        return await _db.UserNotes.FirstOrDefaultAsync(un => un.UserId == id);
    }

    public async Task UpdateNote(ulong id, string note)
    {
        var un = await GetUserNote(id);
        if (un == null)
        {
            un = new UserNote() { UserId = id};
            await _db.UserNotes.AddAsync(un);
        }
        un.Note = note;
        await _db.SaveChangesAsync();
    }
}