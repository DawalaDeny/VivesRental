using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace Vives.SDK
{
    public class CustomerManagementSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerManagementSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<CustomerResult>> Find()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Customer";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var customers = await response.Content.ReadFromJsonAsync<IList<CustomerResult>>();
            if (customers is null)
            {
                return new List<CustomerResult>();
            }

            return customers;
        }
        public async Task<CustomerResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Customer/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }
        public async Task<CustomerResult?> CreateAsync(CustomerRequest customer)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Customer";
            var response = await client.PostAsJsonAsync(route, customer);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Customer/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
        public async Task<CustomerResult?> UpdateAsync(Guid id, CustomerRequest request)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Customer/{id}";
            var response = await client.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }
    }
}