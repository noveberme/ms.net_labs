using TicketsFilm.BL.Users.Entity;

namespace TicketsFilm.BL.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(UserFilterModels filter = null);
    UserModel GerUserInfo(int id);
}