using System.ComponentModel.DataAnnotations;

namespace GhostRunnerStaffProfile.Data.Models;

public class UserNote
{
    [Key]
    public ulong UserId { get; set; }
    [MaxLength(50)]
    public string Note { get; set; }
}