using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pocolink.UI.Clients
{
    public class ShorteningClient : IShorteningClient
    {
        private readonly HttpClient _httpClient;

        public ShorteningClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ShortenUrl(string longUrl)
        {
            var data = new { longUrl };

            var requestContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"api/ShortenUrl", requestContent);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<string> RetrieveUrl(string shortUrl)
        {
            var data = new { shortUrl };

            var requestContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/RetrieveSourceUrl", requestContent);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
