using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Tickets")]
public class TicketEntity : BaseEntity
{
    public int Number_Row { get; set; }
    public int Number_Seat { get; set; }
    public string Condition { get; set; }
    
    public int OrderId { get; set; }
    public virtual OrderEntity Order { get; set; }
    
    public int SessionId { get; set; }
    public virtual SessionEntity Session { get; set; }
}