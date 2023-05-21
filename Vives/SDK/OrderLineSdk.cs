using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;
using VivesRental.UI.Models;

namespace VivesRental.UI.SDK
{
    public class OrderLineSdk : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrderLineSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<OrderLineResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
           
            var route = $"api/OrderLine/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderLineResult>();
        }

        public async Task<IList<OrderLineResult>> FindAsync()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/OrderLine";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var orderlines = await response.Content.ReadFromJsonAsync<IList<OrderLineResult>>();
            if (orderlines is null)
            {
                return new List<OrderLineResult>();
            }

            return orderlines;
        }

        public async Task RentAsync(Guid orderId, Guid articleId)
        {
            var rent = new RentSingle();
            rent.articleId = articleId;
            rent.orderId = orderId;
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/OrderLine";
            var response = await client.PostAsJsonAsync(route, rent);

            response.EnsureSuccessStatusCode();

        }
        public async Task RentMultipleAsync(Guid orderId, IList<Guid> articleIds)
        {
            var rent = new RentMultiple();
            rent.articleIds = articleIds;
            rent.orderId = orderId;
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/OrderLine/*";
            var response = await client.PostAsJsonAsync(route, rent);

            response.EnsureSuccessStatusCode();
            
        }
        public async Task ReturnAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/OrderLine/{id}";
            var response = await client.PutAsJsonAsync(route, Empty);

            response.EnsureSuccessStatusCode();

        }
    }
}
