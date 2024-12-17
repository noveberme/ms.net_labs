namespace TicketsFilm.Service.Settings;

public class TicketsFilmSettings
{
    public string ConnectionString { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? IdentityServerUri { get; set; }
}