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

        public async Task<IList<ProductResult>> Find()
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

    }
}