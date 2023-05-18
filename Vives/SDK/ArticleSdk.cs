using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.UI.SDK
{
    public class ArticleSdk
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ArticleSdk(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<ArticleResult?> GetAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Article/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<IList<ArticleResult>> FindAsync()
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Article";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<IList<ArticleResult>>();
            if (articles is null)
            {
                return new List<ArticleResult>();
            }

            return articles;
        }

        public async Task<ArticleResult?> CreateAsync(ArticleRequest request)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = "api/Article";
            var response = await client.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<ArticleResult?> UpdateAsync(Guid id, ArticleRequest request)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Article/{id}";
            var response = await client.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = httpClientFactory.CreateClient("VivesRentalAPI");
            var route = $"api/Article/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
            
        }
    }
}
