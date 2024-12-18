using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using TicketFilm.BL.Authorization;
using TicketFilm.BL.Authorization.Exceptions;
using TicketFilm.BL.Mapper;
using TicketsFilm.DataAccess.Entities;

namespace TicketsFilm.BL.Tests.Authorization;

public class AuthProviderTest
{
    [TestFixture]
    [Category("Authorization")]
    public class AuthProviderTest
    {
        private Mock<SignInManager<UserEntity>> _signInManagerMock;
        private Mock<UserManager<UserEntity>> _userManagerMock;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private IMapper _mapper;
        private AuthProvider _authProvider;

        [SetUp]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<UserEntity>>();
            _userManagerMock = new Mock<UserManager<UserEntity>>(userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
            _signInManagerMock = new Mock<SignInManager<UserEntity>>(
                _userManagerMock.Object, contextAccessorMock.Object, userClaimsPrincipalFactoryMock.Object, null, null, null, null);

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersBlProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _authProvider = new AuthProvider(
                _signInManagerMock.Object,
                _userManagerMock.Object,
                _httpClientFactoryMock.Object,
                "http://identityserver",
                "test-client",
                "test-secret",
                _mapper);
        }

        [Test]
        public async Task AuthorizeUser_ThrowsException_WhenUserNotFound()
        {
            _userManagerMock.Setup(um => um.FindByEmailAsync("invalid@example.com"))
                .ReturnsAsync((UserEntity)null);
            
            Func<Task> act = async () =>
                await _authProvider.AuthorizeUser("invalid@example.com", "password123");

            act.Should().ThrowAsync<Exception>().WithMessage("User not found.");
        }

        [Test]
        public async Task RegisterUser_CreatesUser_WhenDataIsValid()
        {
            var email = "test@example.com";
            var password = "password123@";
            var fullName = "Test User";
            var login = "testlogin";

            _userManagerMock.SetupSequence(um => um.FindByEmailAsync(email))
                .ReturnsAsync((UserEntity)null)
                .ReturnsAsync(new UserEntity());

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<UserEntity>(), password))
                .ReturnsAsync(IdentityResult.Success);
            var result = await _authProvider.RegisterUser(fullName, login, email, password);
            
            result.Should().NotBeNull();
            _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<UserEntity>(), password), Times.Once);
        }

        [Test]
        public async Task RegisterUser_ThrowsAlreadyExistException_WhenUserExists()
        {
            var email = "existing@example.com";
            _userManagerMock.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync(new UserEntity());
            
            Func<Task> act = async () =>
                await _authProvider.RegisterUser("Test", "testlogin", email, "password123");

            act.Should().ThrowAsync<AlreadyExistException>().WithMessage("User already exists.");
        }
    }
}