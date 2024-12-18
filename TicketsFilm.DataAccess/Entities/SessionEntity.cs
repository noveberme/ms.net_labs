using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Session")]
public class SessionEntity : BaseEntity
{
    public int Price { get; set; }

    public int FilmId { get; set; }
    public virtual FilmEntity Film { get; set; }

    public int HallId { get; set; }
    public virtual HallEntity Hall { get; set; }

    /*public int CinemaId { get; set; }
    public virtual CinemaEntity Cinema { get; set; }*/

    public virtual ICollection<TicketEntity>? Tickets { get; set; }
}