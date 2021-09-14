using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ExampleApp.Client.Data
{
    public class AuthenticationUserService : IAuthenticateUserService
    {
        private readonly IHttpService _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private const string ROUTE = "/api/authenticate/";
        private string _userKey = "user";

        public AuthenticationUserService(IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthUser> Login(LoginModel model)
        {
            var tokenResponse = await _httpClient.Post<AuthUser>($"{ROUTE}login", model);
            if (tokenResponse == null)
                return tokenResponse;
            await _localStorageService.SetItemAsync("authToken", tokenResponse.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(tokenResponse.UserName);

            return tokenResponse;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();         
        }

        public async Task<Response> Register(RegisterModel model)
        {
            var response = await _httpClient.Post<Response>($"{ROUTE}register", model);          
            return response;
        }

    }
}