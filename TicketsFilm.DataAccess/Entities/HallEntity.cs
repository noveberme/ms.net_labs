using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Hall")]
public class HallEntity : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }

    public int CinemaId { get; set; }
    public virtual CinemaEntity Cinema { get; set; }

    public virtual ICollection<SessionEntity>? Sessions { get; set; }
}