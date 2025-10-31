using System.Net.Http.Json;
using EVBattery.Infrastructure.Config;

namespace EVBattery.Infrastructure.Http
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient? client = null)
        {
            _http = client ?? new HttpClient { BaseAddress = new Uri(ApiConfig.BaseUrl) };
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var res = await _http.GetAsync(url);
            return res.IsSuccessStatusCode
                ? await res.Content.ReadFromJsonAsync<T>()
                : default;
        }

        public async Task<T?> PostAsync<T>(string url, object data)
        {
            var res = await _http.PostAsJsonAsync(url, data);
            return res.IsSuccessStatusCode
                ? await res.Content.ReadFromJsonAsync<T>()
                : default;
        }

        public async Task<T?> PatchAsync<T>(string url, object data)
        {
            var req = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = JsonContent.Create(data)
            };
            var res = await _http.SendAsync(req);
            return res.IsSuccessStatusCode
                ? await res.Content.ReadFromJsonAsync<T>()
                : default;
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var res = await _http.DeleteAsync(url);
            return res.IsSuccessStatusCode;
        }
    }
}
