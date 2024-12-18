using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsFilm.DataAccess.Entities;

[Table("Film")]
public class FilmEntity : BaseEntity
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string Main_actors { get; set; }
    public DateTime Duration { get; set; }

    public virtual ICollection<SessionEntity>? Sessions { get; set; }
}