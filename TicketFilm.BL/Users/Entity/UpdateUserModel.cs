namespace TicketFilm.BL.Users.Entity;

public class UpdateUserModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Numberphone { get; set; }
    public string PasswordHash { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
}