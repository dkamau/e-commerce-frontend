using Blazored.SessionStorage;
using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Authentication;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class HttpService
    {
        ISessionStorageService _sessionStorage;
        HttpClient httpClient;
        public HttpService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<HttpResponse> UploadAsync(string endpoint, MultipartFormDataContent data)
        {
            try
            {
                await InitializeHttpClient();
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(endpoint, data);
                HttpResponse httpResponse = await GetHttpResponse(httpResponseMessage);

                return httpResponse;
            }
            catch (Exception ex)
            {
                return new HttpResponse()
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
            }
        }

        public async Task<HttpResponse> PostAsync(string endpoint, object data)
        {
            try
            {
                await InitializeHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(endpoint, content);
                HttpResponse httpResponse = await GetHttpResponse(httpResponseMessage);

                return httpResponse;
            }
            catch(Exception ex)
            {
                return new HttpResponse()
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
            }
        }

        public async Task<HttpResponse> PatchAsync(string endpoint, object data)
        {
            try
            {
                await InitializeHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PatchAsync(endpoint, content);
                HttpResponse httpResponse = await GetHttpResponse(httpResponseMessage);

                return httpResponse;
            }
            catch (Exception ex)
            {
                return new HttpResponse()
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
            }
        }

        public async Task<HttpResponse> GetAsync(string endpoint)
        {
            try
            {
                await InitializeHttpClient();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endpoint);
                HttpResponse httpResponse = await GetHttpResponse(httpResponseMessage);
                return httpResponse;
            }
            catch (Exception ex)
            {
                return new HttpResponse()
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
            }
        }

        public async Task<HttpResponse> DeleteAsync(string endpoint)
        {
            try
            {
                await InitializeHttpClient();
                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(endpoint);
                HttpResponse httpResponse = await GetHttpResponse(httpResponseMessage);
                return httpResponse;
            }
            catch (Exception ex)
            {
                return new HttpResponse()
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
            }
        }

        private async Task<HttpResponse> GetHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            string result = await httpResponseMessage.Content.ReadAsStringAsync();

            HttpResponse httpResponse = new HttpResponse()
            {
                HttpStatusCode = httpResponseMessage.StatusCode,
                Success = httpResponseMessage.IsSuccessStatusCode,
                Data = result
            };

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    httpResponse.Data = "The resource you requested could not be found.";
                }
                else
                {
                    try
                    {
                        ApiError apiError = JsonConvert.DeserializeObject<ApiError>(result);
                        if (apiError != null)
                            httpResponse.Data = string.IsNullOrEmpty(apiError.Message) ? apiError.Title : apiError.Message;
                    }
                    catch (Exception)
                    {
                        httpResponse.Data = "Opps! An error occured while processing your request.";
                    }
                }
            }

            return httpResponse;
        }

        private async Task InitializeHttpClient()
        {
            string backendUrl = Environment.GetEnvironmentVariable("ECommerce_BACKEND_LINK");
            if (string.IsNullOrEmpty(backendUrl))
                backendUrl = "http://localhost:57678/";

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(backendUrl);

            var sessionUser = await _sessionStorage.GetItemAsync<CurrentUser>(StorageConstants.StoredUser);
            if (sessionUser != null)
            {
                CurrentUser loggedInUser = sessionUser;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedInUser.Token);
            }
        }
    }

    public class HttpResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string Data { get; set; }
    }

    public class ApiError
    {
        public int? StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ErrorCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
