using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Authentication;
using ECommerceFrontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceFrontend
{
    public class CustomAuthonticationStateProvider : AuthenticationStateProvider
    {
        ProtectedSessionStorage _protectedSessionStorage;
        ProtectedLocalStorage _protectedLocalStorage;
        AuthenticationService _authenticationService;
        public CustomAuthonticationStateProvider(
            ProtectedSessionStorage protectedSessionStorage, 
            ProtectedLocalStorage protectedLocalStorage,
            AuthenticationService authenticationService)
        {
            _protectedSessionStorage = protectedSessionStorage;
            _protectedLocalStorage = protectedLocalStorage;
            _authenticationService = authenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var sessionUser = await _protectedSessionStorage.GetAsync<CurrentUser>(StorageConstants.StoredUser);
            if (sessionUser.Success)
            {
                CurrentUser loggedInUser = sessionUser.Value;
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
                await _protectedSessionStorage.SetAsync(StorageConstants.StoredUser, loggedInUser);
                await SetRememberMe(email, password, rememberMe);

                SetClaims(email);

                return authenticationResult;
            }

            return authenticationResult;
        }

        public async Task LogoutUser()
        {
            await _protectedSessionStorage.DeleteAsync(StorageConstants.StoredUser);
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
                await _protectedLocalStorage.SetAsync(StorageConstants.StoredEmail, email);
                await _protectedLocalStorage.SetAsync(StorageConstants.StoredPassword, password);
            }
            else
            {
                await _protectedLocalStorage.DeleteAsync(StorageConstants.StoredEmail);
                await _protectedLocalStorage.DeleteAsync(StorageConstants.StoredPassword);
            }
        }
    }
}
