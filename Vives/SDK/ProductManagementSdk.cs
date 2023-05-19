using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace Vives.SDK
{
    public class ProductManagementSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductManagementSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ProductResult>> FindAsync()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Product";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<IList<ProductResult>>();
            if (products is null)
            {
                return new List<ProductResult>();
            }

            return products;
        }
        public async Task<ProductResult?> CreateAsync(ProductRequest product)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Product";
            var response = await client.PostAsJsonAsync(route, product);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }
        public async Task<ProductResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Product/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Product/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
        public async Task<ProductResult?> UpdateAsync(Guid id, ProductRequest request)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Product/{id}";
            var response = await client.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

      
      
        public async Task GenerateArticlesAsync(Guid id, int amount)
        {

            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Product/{id}/{amount}";
            var response = await client.PostAsJsonAsync(route, amount);
            
            response.EnsureSuccessStatusCode();
            
        }
    }
}