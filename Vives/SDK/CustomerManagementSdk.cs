using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using VivesRental.Model;

namespace Vives.SDK
{
    public class CustomerManagementSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerManagementSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Customer>> Find()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Customer";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var customers = await response.Content.ReadFromJsonAsync<IList<Customer>>();
            if (customers is null)
            {
                return new List<Customer>();
            }

            return customers;
        }
        public async Task<Customer?> Create(Customer customer)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Customer";
            var response = await client.PostAsJsonAsync(route, customer);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Customer>();
        }

    }
}