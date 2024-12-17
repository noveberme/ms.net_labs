using TicketFilm.BL.Authorization.Entities;
using TicketsFilm.BL.Users.Entity;

namespace TicketFilm.BL.Authorization;

public interface IAuthProvider
{
    Task<UserModel> RegisterUser(string username, string email, string password);
    Task<TokensResponse> AuthorizeUser(string email, string password);
}