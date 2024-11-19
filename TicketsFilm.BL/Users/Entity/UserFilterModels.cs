namespace TicketsFilm.BL.Users.Entity;

public class UserFilterModels
{
    public string? UsernamePart { get; set; }
    public string? EmailPart { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? Role { get; set; }
    public string Numberphone { get; set; }
    public int Age { get; set; }
}