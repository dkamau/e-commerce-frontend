using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Orders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class CheckoutService
    {
        HttpService _httpService;
        public CheckoutService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<OrderHttpResult> CreateAsync(Order order)
        {
            var result = await _httpService.PostAsync($"{Endpoints.Orders}", order);

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (orderHttpResult.Success)
                orderHttpResult.Order = JsonConvert.DeserializeObject<Order>(result.Data);

            return orderHttpResult;
        }

        public async Task<OrderHttpResult> UploadImageAsync(int orderId, MultipartFormDataContent data)
        {
            var result = await _httpService.UploadAsync($"{Endpoints.Orders}/{orderId}/UploadImage", data);

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (orderHttpResult.Success)
                orderHttpResult.Order = JsonConvert.DeserializeObject<Order>(result.Data);

            return orderHttpResult;
        }

        public async Task<OrderHttpResult> ListAsync()
        {
            var result = await _httpService.GetAsync($"{Endpoints.Orders}/List?Paginate=false");

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (orderHttpResult.Success)
                orderHttpResult.Orders = JsonConvert.DeserializeObject<List<Order>>(result.Data);

            return orderHttpResult;
        }

        public async Task<OrderHttpResult> GetAsync(int orderId)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Orders}/{orderId}");

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (orderHttpResult.Success)
                orderHttpResult.Order = JsonConvert.DeserializeObject<Order>(result.Data);

            return orderHttpResult;
        }

        public async Task<OrderHttpResult> UpdateAsync(Order order)
        {
            var result = await _httpService.PatchAsync($"{Endpoints.Orders}", order);

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (orderHttpResult.Success)
                orderHttpResult.Order = JsonConvert.DeserializeObject<Order>(result.Data);

            return orderHttpResult;
        }

        public async Task<OrderHttpResult> DeleteAsync(int orderId)
        {
            var result = await _httpService.DeleteAsync($"{Endpoints.Orders}/{orderId}");

            OrderHttpResult orderHttpResult = new OrderHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            return orderHttpResult;
        }
    }

    public class OrderHttpResult
    {
        public bool Success { get; set; }
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public string Message { get; set; }
    }
}

