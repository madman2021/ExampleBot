using GhostRunnerStaffProfile.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GhostRunnerStaffProfile.Data;

public class Db : DbContext
{
    private readonly BotConfig _config;

    public Db(BotConfig config)
    {
        _config = config;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var location = _config.DbLocation;
        if (!Directory.Exists(Path.GetDirectoryName(location)))
        {
            throw new ArgumentException("Database path doesn't exist");
        }
        optionsBuilder.UseSqlite($"Data Source={location}");
    }
    
    public DbSet<UserNote?> UserNotes { get; set; }
}