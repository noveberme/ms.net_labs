namespace TicketsFilm.Service.Controllers.Users.Entities;

public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Numberphone { get; set; }
    public string? Age { get; set; }
    
    public int RoleId { get; set; }
}