using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using VivesRental.Model;
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
        public async Task<CustomerResult?> Create(CustomerRequest customer)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Customer";
            var response = await client.PostAsJsonAsync(route, customer);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

    }
}