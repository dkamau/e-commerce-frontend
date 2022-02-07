using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class AuthenticationService
    {
        HttpService _httpService;
        public AuthenticationService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<AuthenticationResult> Authenticate(string email, string password)
        {
            var result = await _httpService.PostAsync($"{Endpoints.Authentication}/Login", new UserSignIn()
            {
                Email = email,
                Password = password
            });

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }

        public async Task<AuthenticationResult> Signup(UserSignUp userSignUp)
        {
            var result = await _httpService.PostAsync($"{Endpoints.Authentication}/Signup", userSignUp);

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }

        public async Task<AuthenticationResult> ConfirmAccount(string id)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Authentication}/ConfirmAccount/{id}");

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }

        public async Task<AuthenticationResult> ResendConfirmationLink(string email)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Authentication}/ResendConfirmationLink/{email}");

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }

        public async Task<AuthenticationResult> SendResetPasswordLink(string email)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Authentication}/SendResetPasswordLink/{email}");

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }

        public async Task<AuthenticationResult> ResetPassword(UserResetPassword user)
        {
            var result = await _httpService.PostAsync($"{Endpoints.Authentication}/ResetPassword", user);

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (authenticationResult.Success)
                authenticationResult.CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(result.Data);

            return authenticationResult;
        }
    }

    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public string Message { get; set; }
    }
}
