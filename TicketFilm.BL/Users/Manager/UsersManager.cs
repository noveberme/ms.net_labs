using AutoMapper;
using TicketFilm.BL.Users.Entity;
using TicketFilm.BL.Users.Exceptions;
using TicketFilm.BL.Users.Manager;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;

namespace BeautyShopBL.Users.Manager;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;
    public UsersManager(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public UserModel CreateUser(CreateUserModel createModel)
    {
        var entity = _mapper.Map<UserEntity>(createModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
    public void DeleteUser(int id)
    {
        try
        {
            var entity = _usersRepository.GetById(id);
            _usersRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new UserNotFoundException(e.Message);
        }
    }
    public UserModel UpdateUser(UpdateUserModel updateModel)
    {
        var entity = _mapper.Map<UserEntity>(updateModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}
