using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
 
namespace TicketsFilm.DataAccess.Entities;

[Table("User")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    private IBaseEntity _baseEntityImplementation;
    public string Username { get; set; }
    public string E_mail { get; set; }
    public string Number_phone { get; set; }
    public string PasswordHash { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
    
    public virtual ICollection<OrderEntity>? Orders { get; set; }
}
public partial class UserRole : IdentityRole<int>
{
}