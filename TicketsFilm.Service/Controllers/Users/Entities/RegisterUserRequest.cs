namespace TicketsFilm.Service.Controllers.Users.Entities;

public class RegisterUserRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RoleId { get; set; }
    public string Numberphone { get; set; }
    public int Age { get; set; }
}