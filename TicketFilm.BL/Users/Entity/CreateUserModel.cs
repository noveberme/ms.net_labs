namespace TicketFilm.BL.Users.Entity;

public class CreateUserModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Numberphone { get; set; }
    public string PasswordHash { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
}
