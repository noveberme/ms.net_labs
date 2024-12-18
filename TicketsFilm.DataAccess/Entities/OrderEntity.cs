using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Order")]
public class OrderEntity : BaseEntity
{
    public DateTime Date { get; set; }
    public int General_price { get; set; }

    public virtual ICollection<TicketEntity>? Tickets { get; set; }

    public int UserId { get; set; }
    public virtual UserEntity User { get; set; }
}