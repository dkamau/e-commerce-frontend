using Blazored.LocalStorage;
using Blazored.SessionStorage;
using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Authentication;
using ECommerceFrontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceFrontend
{
    public class CustomAuthonticationStateProvider : AuthenticationStateProvider
    {
        ISessionStorageService _sessionStorage;
        ILocalStorageService _localStorage;
        AuthenticationService _authenticationService;
        public CustomAuthonticationStateProvider(
            ISessionStorageService sessionStorage, 
            ILocalStorageService localStorage,
            AuthenticationService authenticationService)
        {
            _sessionStorage = sessionStorage;
            _localStorage = localStorage;
            _authenticationService = authenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var sessionUser = await _sessionStorage.GetItemAsync<CurrentUser>(StorageConstants.StoredUser);
            if (sessionUser != null)
            {
                CurrentUser loggedInUser = sessionUser;
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, loggedInUser.Email),
                }, "apiauth_type");
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task<AuthenticationResult> ValidateUser(string email, string password, bool rememberMe)
        {
            email = email.Trim();
            password = password.Trim();

            AuthenticationResult authenticationResult = await _authenticationService.Authenticate(email, password);
            if (authenticationResult.Success)
            {
                CurrentUser loggedInUser = authenticationResult.CurrentUser;
                await _sessionStorage.SetItemAsync(StorageConstants.StoredUser, loggedInUser);
                await SetRememberMe(email, password, rememberMe);

                SetClaims(email);

                return authenticationResult;
            }

            return authenticationResult;
        }

        public async Task LogoutUser()
        {
            await _sessionStorage.RemoveItemAsync(StorageConstants.StoredUser);
            var user = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private void SetClaims(string email)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private async Task SetRememberMe(string email, string password, bool rememberMe)
        {
            if (rememberMe)
            {
                await _localStorage.SetItemAsync(StorageConstants.StoredEmail, email);
                await _localStorage.SetItemAsync(StorageConstants.StoredPassword, password);
            }
            else
            {
                await _localStorage.RemoveItemAsync(StorageConstants.StoredEmail);
                await _localStorage.RemoveItemAsync(StorageConstants.StoredPassword);
            }
        }
    }
}
