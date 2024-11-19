using AutoMapper;
using TicketsFilm.BL.Provider;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.BL.Users.Exceptions;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;

namespace TicketsFilm.BL.Users.Provider;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private IUsersProvider _usersProviderImplementation;

    public UsersProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public IEnumerable<UserModel> GetUsers(UserFilterModels filter = null)
    {
        string? namePart = filter?.UsernamePart;
        string? emailPart = filter?.EmailPart;
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        string? role = filter?.Role;
        string? numberphone = filter?.Numberphone;
        int? age = filter?.Age;
        var users = _userRepository.GetAll(u =>
            (namePart == null || u.Username.Contains(namePart)) &&
            (emailPart == null || u.E_mail.Contains(emailPart)) &&
            (creationTime == null || u.CreationTime == creationTime) &&
            (modificationTime == null || u.ModificationTime == modificationTime) &&
            (role == null || u.Role == role) &&
            (numberphone == null || u.Number_phone == numberphone) &&
            (age == null || u.Age == age)
        );
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }
    public UserModel GerUserInfo(int id)
    {
        var entity = _userRepository.GetById(id);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }
        return _mapper.Map<UserModel>(entity);
    }
}