namespace TicketsFilm.Service.Controllers.Users.Entities;

public class UserFilter
{
    public string? UsernamePart { get; set; }
    public string? EmailPart { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? Role { get; set; }
    public string? Numberphone { get; set; }
    public int? Age { get; set; }
}