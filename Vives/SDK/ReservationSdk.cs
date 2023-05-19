using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.UI.SDK
{
    public class ReservationSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ReservationSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ArticleReservationResult>> FindAsync()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/ArticleReservation";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<IList<ArticleReservationResult>>();
            if (reservations is null)
            {
                return new List<ArticleReservationResult>();
            }
            return reservations;
        }
        public async Task<ArticleReservationResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/ArticleReservation/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }
        public async Task<ArticleReservationResult?> CreateAsync(ArticleReservationRequest articleReservation)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "/api/ArticleReservation";
            var response = await client.PostAsJsonAsync(route, articleReservation);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"/api/ArticleReservation/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}