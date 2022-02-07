using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Products;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class ProductStockUpdateService
    {
        HttpService _httpService;
        public ProductStockUpdateService(HttpService httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// Creates a new transaction
        /// </summary>
        /// <param name="productStockUpdate"></param>
        /// <returns></returns>
        public async Task<ProductStockUpdateHttpResult> CreateAsync(ProductStockUpdate productStockUpdate, ProductStockUpdateType productStockUpdateType)
        {
            HttpResponse result;
            if(productStockUpdateType == ProductStockUpdateType.Addition)
                result = await _httpService.PostAsync($"{Endpoints.ProductStockUpdates}/AddStock", productStockUpdate);
            else
                result = await _httpService.PostAsync($"{Endpoints.ProductStockUpdates}/DeductStock", productStockUpdate);

            ProductStockUpdateHttpResult productStockUpdateHttpResult = new ProductStockUpdateHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productStockUpdateHttpResult.Success)
                productStockUpdateHttpResult.ProductStockUpdate = JsonConvert.DeserializeObject<ProductStockUpdate>(result.Data);

            return productStockUpdateHttpResult;
        }

        /// <summary>
        /// Gets a list of all product transactions
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<ProductStockUpdateHttpResult> ListAsync(int productId, string startDate = null, string endDate = null)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Products}/{productId}{Endpoints.ProductStockUpdates}?StartDate={startDate}&EndDate={endDate}");

            ProductStockUpdateHttpResult productStockUpdateHttpResult = new ProductStockUpdateHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productStockUpdateHttpResult.Success)
                productStockUpdateHttpResult.ProductStockUpdates = JsonConvert.DeserializeObject<List<ProductStockUpdate>>(result.Data);

            return productStockUpdateHttpResult;
        }

        /// <summary>
        /// Gets a list of all user transactions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<ProductStockUpdateHttpResult> ListAllAsync(int userId, string startDate = null, string endDate = null)
        {
            var result = await _httpService.GetAsync($"{Endpoints.ProductStockUpdates}?StartDate={startDate}&EndDate={endDate}");

            ProductStockUpdateHttpResult productStockUpdateHttpResult = new ProductStockUpdateHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productStockUpdateHttpResult.Success)
                productStockUpdateHttpResult.ProductStockUpdates = JsonConvert.DeserializeObject<List<ProductStockUpdate>>(result.Data);

            return productStockUpdateHttpResult;
        }

        /// <summary>
        /// Gets a summary of all product transactions for the given date.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate">dd-MMM-yyyy</param>
        /// <param name="endDate">dd-MMM-yyyy</param>
        /// <returns></returns>
        public async Task<ProductStockUpdateHttpResult> GetSummaryAsync(int userId, string startDate = null, string endDate = null)
        {
            var result = await _httpService.GetAsync($"{Endpoints.ProductStockUpdates}/{userId}/Summary?StartDate={startDate}&EndDate={endDate}");

            ProductStockUpdateHttpResult productStockUpdateHttpResult = new ProductStockUpdateHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productStockUpdateHttpResult.Success)
                productStockUpdateHttpResult.ProductStockUpdateSummary = JsonConvert.DeserializeObject<ProductStockUpdateSummary>(result.Data);

            return productStockUpdateHttpResult;
        }
    }

    public class ProductStockUpdateHttpResult
    {
        public bool Success { get; set; }
        public ProductStockUpdateSummary ProductStockUpdateSummary { get; set; }
        public ProductStockUpdate ProductStockUpdate { get; set; }
        public List<ProductStockUpdate> ProductStockUpdates { get; set; }
        public string Message { get; set; }
    }
}
