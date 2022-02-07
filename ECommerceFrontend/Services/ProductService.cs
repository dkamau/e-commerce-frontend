using ECommerceFrontend.Constants;
using ECommerceFrontend.Models.Products;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class ProductService
    {
        HttpService _httpService;
        public ProductService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ProductHttpResult> CreateAsync(Product product)
        {
            var result = await _httpService.PostAsync($"{Endpoints.Products}", product);

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productHttpResult.Success)
                productHttpResult.Product = JsonConvert.DeserializeObject<Product>(result.Data);

            return productHttpResult;
        }

        public async Task<ProductHttpResult> UploadImageAsync(int productId, MultipartFormDataContent data)
        {
            var result = await _httpService.UploadAsync($"{Endpoints.Products}/{productId}/UploadImage", data);

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productHttpResult.Success)
                productHttpResult.Product = JsonConvert.DeserializeObject<Product>(result.Data);

            return productHttpResult;
        }

        public async Task<ProductHttpResult> ListAsync()
        {
            var result = await _httpService.GetAsync($"{Endpoints.Products}/List?Paginate=false");

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productHttpResult.Success)
                productHttpResult.Products = JsonConvert.DeserializeObject<List<Product>>(result.Data);

            return productHttpResult;
        }

        public async Task<ProductHttpResult> GetAsync(int productId)
        {
            var result = await _httpService.GetAsync($"{Endpoints.Products}/{productId}");

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productHttpResult.Success)
                productHttpResult.Product = JsonConvert.DeserializeObject<Product>(result.Data);

            return productHttpResult;
        }

        public async Task<ProductHttpResult> UpdateAsync(Product product)
        {
            var result = await _httpService.PatchAsync($"{Endpoints.Products}", product);

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            if (productHttpResult.Success)
                productHttpResult.Product = JsonConvert.DeserializeObject<Product>(result.Data);

            return productHttpResult;
        }

        public async Task<ProductHttpResult> DeleteAsync(int productId)
        {
            var result = await _httpService.DeleteAsync($"{Endpoints.Products}/{productId}");

            ProductHttpResult productHttpResult = new ProductHttpResult()
            {
                Success = result.Success,
                Message = result.Data,
            };

            return productHttpResult;
        }
    }

    public class ProductHttpResult
    {
        public bool Success { get; set; }
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
        public string Message { get; set; }
    }
}

