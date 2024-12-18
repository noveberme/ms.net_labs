using AutoMapper;
using Duende.IdentityServer.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using TicketFilm.BL.Authorization.Entities;
using TicketFilm.BL.Authorization.Exceptions;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace TicketFilm.BL.Authorization;

public class AuthProvider (
    SignInManager<UserEntity> signInManager, 
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    string identityServerUri,
    string clientId,
    string clientSecret, IMapper mapper) : IAuthProvider
    {
        private IAuthProvider _authProviderImplementation;

        public Task<UserModel> RegisterUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<TokensResponse> AuthorizeUser(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new Exception();
            }

            var verificationPasswordResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!verificationPasswordResult.Succeeded)
            {
                throw new Exception();
            }

            var client = httpClientFactory.CreateClient();
            var discoveryDoc = await client.GetDiscoveryDocumentAsync(identityServerUri);
            if (discoveryDoc.IsError)
            {
                throw new Exception();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientId = clientId,
                ClientSecret = clientSecret,
                UserName = user.UserName,
                Password = password,
                Scope = "api offline_access"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception();
            }

            return new TokensResponse
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
            };
        }

        public async Task<UserModel> RegisterUser(string username, string email, string password, string password123)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                throw new AlreadyExistException();
            }

            var user = new UserEntity
            {
                Username = username,
                Email = email,
                UserName = email,
            };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new UserCreationException();
            }

            var createUserResult = await userManager.CreateAsync(user, password);
            return mapper.Map<UserModel>(createUserResult);
        }
    }