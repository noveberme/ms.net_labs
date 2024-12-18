using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;
using TicketsFilmUnitTests.TicketsFilmApiEndpoints;

namespace TestProject1.UserAuthorization;

public class LoginUserTest
{
    [Test]
    public async Task BadRequestUserNotFoundResultTest()
    {
        var login = "not_existing@mail.ru";
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var user = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == login.ToLower());
        if (user != null)
        {
            userRepository.Delete(user);
        }

        var password = "password";
        var query = $"?email={login}&password={password}";
        var requestUri =
            TicketsFilmApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await TestHttpClient.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PasswordIsIncorrectResultTest()
    {
        var user = new UserEntity()
        {
            Email = "test@test",
            UserName = "test@test",
        };
        var password = "password";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
        await userManager.CreateAsync(user, password);

        var incorrect_password = "kjdffsd";
        
        var query = $"?email={user.UserName}&password={incorrect_password}";
        var requestUri =
            TicketsFilmServiceApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest); 
    }

    [Test]
    [TestCase("", "")]
    [TestCase("qwe", "")]
    [TestCase("test@test", "")]
    [TestCase("", "password")]
    public async Task LoginOrPasswordAreInvalidResultTest(string login, string password)
    {
        var query = $"?login={login}&password={password}";
        var requestUri =
            TicketsFilmServiceApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest); 
    }

}