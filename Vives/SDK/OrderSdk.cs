using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.UI.SDK
{
    public class OrderSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrderSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<OrderResult>> FindAsync()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Order";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<IList<OrderResult>>();
            if (orders is null)
            {
                return new List<OrderResult>();
            }

            return orders;
        }

        public async Task<OrderResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Order/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<OrderResult?> CreateAsync(Guid customerid)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Order";
            var response = await client.PostAsJsonAsync(route, customerid);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<OrderResult?> UpdateAsync(Guid id, DateTime returnedAt)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Order/{id}";
            var response = await client.PutAsJsonAsync(route, returnedAt);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

    }
}