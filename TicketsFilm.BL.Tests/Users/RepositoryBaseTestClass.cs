using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketsFilm.DataAccess;

namespace TicketsFilm.BL.Tests.Users;

public class RepositoryBaseTestClass
{
    public RepositoryBaseTestClass()
    {

        ServiceProvider = ConfigureServiceProvider();
        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>();

  
        var dbContext = DbContextFactory.CreateDbContext();
        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>(); 
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
      
        serviceCollection.AddDbContextFactory<TicketsFilmDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        
        return serviceCollection.BuildServiceProvider();
    }

    protected readonly IServiceProvider ServiceProvider;
    protected readonly IDbContextFactory<TicketsFilmDbContext> DbContextFactory;
}