using AutoMapper;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.BL.Users.Manager;
using TicketsFilm.BL.Users.Provider;
using Microsoft.AspNetCore.Mvc;
using TicketsFilm.Service.Controllers.Users.Entities;
using TicketsFilm.Service.Controllers.Validation;

namespace TicketsFilm.Service.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;

    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider,
        IMapper mapper, Serilog.ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var createUserModel = _mapper.Map<CreateUserModel>(request);
            var userModel = _usersManager.CreateUser(createUserModel);
            return Ok(userModel);
        }
        
        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersResponse()
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<UserFilterModels>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersResponse()
        {
            Users = users.ToList()
        });
    }
}