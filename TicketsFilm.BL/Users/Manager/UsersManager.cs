using AutoMapper;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.BL.Users.Exceptions;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;

namespace TicketsFilm.BL.Users.Manager;

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

    public UserModel UpdateUser(UpdateUserModel model, int idUser)
    {
        var entity = _usersRepository.GetById(idUser);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        entity = _mapper.Map<UpdateUserModel, UserEntity>(model, opts => 
            opts.AfterMap(
                (src, dest) =>
                {
                    dest.Id = idUser;
                    dest.ExternalId = entity.ExternalId;
                    dest.CreationTime = entity.CreationTime;
                    dest.ModificationTime=DateTime.UtcNow;
                    dest.Username = src.Username ?? entity.Username;
                    dest.PasswordHash = src.PasswordHash ?? entity.PasswordHash;
                    dest.Email = src.Email ?? entity.Email;
                }
            ));
        
        _usersRepository.Save(entity);
        return  _mapper.Map<UserModel>(entity);
    }
}
