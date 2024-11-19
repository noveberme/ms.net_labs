using TicketFilm.BL.Users.Entity;

namespace TicketFilm.BL.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(UserFilterModels filter = null);
    UserModel GerUserInfo(int id);
}