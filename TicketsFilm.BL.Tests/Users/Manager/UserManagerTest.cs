using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.BL.Users.Manager;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;

namespace TicketsFilm.BL.Tests.Users.Manager;

public class UserManagerTest
{
     [TestFixture]
    [TestOf(typeof(UserManager<>))]
    [Category("Unit")]
    public class UserManagerTest
    {
        private IMapper _mapper;
        private Mock<IRepository<UserEntity>> _repositoryMock;
        private UsersManager _manager;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserModel, UserEntity>();
                cfg.CreateMap<UpdateUserModel, UserEntity>();
                cfg.CreateMap<UserEntity, UserModel>();
            });
            _mapper = mapperConfig.CreateMapper();

            _repositoryMock = new Mock<IRepository<UserEntity>>();
            _manager = new UsersManager(_repositoryMock.Object, _mapper);
        }

        [SetUp]
        public void Setup()
        {
            _repositoryMock.Reset();
        }

        [Test]
        public void CreateUser_ShouldAddUserToRepository()
        {
            var createModel = new CreateUserModel
            {
                Email = "test@example.com",
                Username = "Test User"
            };

            var userEntity = new UserEntity
            {
                Id = 0,
                Username = createModel.Username,
                Email = createModel.Email
            };
            
            _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(user => user.Id = 1)  
                .Returns((UserEntity user) => user); 
            
            var result = _manager.CreateUser(createModel);
            
            _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
            result.Id.Should().Be(1);
            result.Username.Should().Be("Test User");
            result.Email.Should().Be("test@example.com");
        }

        [Test]
        public void DeleteUser_ShouldRemoveUserFromRepository()
        {
            var user = new UserEntity
            {
                Id = 1,
                Username = "delete_user",
                Email = "delete@example.com",
                CreationTime = DateTime.UtcNow
            };

            _repositoryMock.Setup(repo => repo.GetById(1)).Returns(user);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<UserEntity>()));
            
            _manager.DeleteUser(1);
            
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<UserEntity>()), Times.Once);
        }

        [Test]
        public void UpdateUser_ShouldModifyExistingUser()
        {
            var user = new UserEntity
            {
                Id = 2,
                Username = "Old Name",
                Email = "update@example.com",
                CreationTime = DateTime.UtcNow
            };

            var updateModel = new UpdateUserModel
            {
                Username= "Updated Name",
                Email = "updated@example.com"
            };

            _repositoryMock.Setup(repo => repo.GetById(2)).Returns(user);
            _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(u => 
                {
                    u.Username = updateModel.Username;
                    u.Email = updateModel.Email;
                })
                .Returns((UserEntity u) => u);

            var updateUserModel = _manager.UpdateUser(updateModel, 2);
            
            _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
            updateUserModel.Username.Should().Be("Updated Name");
            updateUserModel.Email.Should().Be("updated@example.com");
        }
    }
}