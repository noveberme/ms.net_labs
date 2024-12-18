using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TestProject1.Helpers;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using TicketsFilm.DataAccess;

namespace TestProject1;

public class TicketsFilmServiceTestBaseClass
{
    public TicketsFilmServiceTestBaseClass()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new WebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var cinemaRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<TicketsFilmDbContext>();
    }

    public T? GetService<T>()
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    private readonly WebApplicationFactory _testServer;
    protected int TestUserId;
    protected HttpClient TestHttpClient => _testServer.CreateClient();
}