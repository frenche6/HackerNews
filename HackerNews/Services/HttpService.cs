using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.Services.Interfaces;

namespace HackerNews.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetStringAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}