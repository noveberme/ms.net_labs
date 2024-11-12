using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Cinema")]
public class CinemaEntity : BaseEntity
{
    public string Adress { get; set; }
    public string E_mail { get; set; }
    
    public virtual ICollection<HallEntity>? Halls { get; set; }
}