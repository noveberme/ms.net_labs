using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicketsFilm.DataAccess.Entities;

[Table("User")]
public class AdminEntity : IdentityUser<int>, IBaseEntity
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public long StateId { get; set; }
}
public partial class UserRole : IdentityRole<int>
{
}