using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Moq;
using TicketFilm.BL.Users.Provider;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.BL.Users.Exceptions;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;

namespace TicketsFilm.BL.Tests.Users.Provider;

public class UserProviderTest
{
    [TestFixture]
    [TestOf(typeof(UsersProvider))]
    public class UserProviderTest
    {
        private Mock<IRepository<UserEntity>> _repositoryMock;
        private IMapper _mapper;
        private UsersProvider _userProvider;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _repositoryMock = new Mock<IRepository<UserEntity>>();
            
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            
            _userProvider = new UsersProvider(_repositoryMock.Object, _mapper);
        }

        [SetUp]
        public void Setup()
        {
            _repositoryMock.Reset();
        }

        [Test]
        public void GetUsers_ReturnsMappedUsers_WhenFilterIsNull()
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, Username = "user1", Email = "user1@test.com" },
                new UserEntity { Id = 2, Username = "user2", Email = "user2@test.com" }
            };
            _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns((Expression<Func<UserEntity, bool>> predicate) =>
                    users.AsQueryable().Where(predicate));

            var result = _userProvider.GetUsers();

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Test]
        public void GetUsers_ReturnsFilteredUsers_WhenFilterIsProvided()
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, Username = "user1", Email = "user1@test.com" },
                new UserEntity { Id = 2, Username = "user2", Email = "user2@test.com" }
            };

            _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns((Expression<Func<UserEntity, bool>> predicate) =>
                    users.AsQueryable().Where(predicate));

            var filter = new UserFilterModels { UsernamePart = "user1" };
            var result = _userProvider.GetUsers(filter);

            result.Should().HaveCount(1);
            result.First().Id.Should().Be(1);
        }

        [Test]
        public void GerUserInfo_ReturnsUser_WhenUserExists()
        {
            var user = new UserEntity { Id = 1, UserName = "user1",  Email = "user1@test.com" };

            _repositoryMock.Setup(repo => repo.GetById(1))
                .Returns(user);

            var result = _userProvider.GerUserInfo(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Username.Should().Be("user1");
        }

        [Test]
        public void GerUserInfo_ThrowsUserNotFoundException_WhenUserDoesNotExist()
        {
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<long>()))
                .Throws(new KeyNotFoundException());

            Assert.Throws<UserNotFoundException>(() => _userProvider.GerUserInfo(1));
        }
    }
}