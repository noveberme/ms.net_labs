using Microsoft.AspNetCore.Identity;
using TicketsFilm.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketsFilm.DataAccess;

public class TicketsFilmDbContext : DbContext
{
    public DbSet<CinemaEntity> Cinemas { get; set; }
    public DbSet<FilmEntity> Films { get; set; }
    public DbSet<HallEntity> Halls { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<AdminEntity> Admins { get; set; }
    
    public TicketsFilmDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
        modelBuilder.Entity<IdentityRole<int>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_roles_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
        
        modelBuilder.Entity<CinemaEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<FilmEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<HallEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<SessionEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TicketEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);

        modelBuilder.Entity<HallEntity>().HasOne(x => x.Cinema)
            .WithMany(x => x.Halls).HasForeignKey(x => x.CinemaId);

        modelBuilder.Entity<SessionEntity>().HasOne(x => x.Hall)
            .WithMany(x => x.Sessions).HasForeignKey(x => x.HallId);
        
        modelBuilder.Entity<SessionEntity>().HasOne(x => x.Film)
            .WithMany(x => x.Sessions).HasForeignKey(x => x.FilmId);
        
        modelBuilder.Entity<OrderEntity>().HasOne(x => x.User)
            .WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<TicketEntity>().HasOne(x => x.Order)
            .WithMany(x => x.Tickets).HasForeignKey(x => x.OrderId);
        
        modelBuilder.Entity<TicketEntity>().HasOne(x => x.Session)
            .WithMany(x => x.Tickets).HasForeignKey(x => x.SessionId);
    }
}