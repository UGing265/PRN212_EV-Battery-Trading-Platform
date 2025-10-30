using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace EVBattery.Core.Helpers
{
    public static class ApiHelper
    {
        private static readonly HttpClient _client = new()
        {
            BaseAddress = new Uri("https://kali.mshiroru.site/")
        };

        public static async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[GET ERROR] {response.StatusCode} → {result}");
                    return default;
                } 
                    
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch
            {
                return default;
            }
        }

        public static async Task<T?> PostAsync<T>(string url, object body)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(url, body);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[GET ERROR] {response.StatusCode} → {result}");
                    return default;
                }
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch
            {
                return default;
            }
        }
    }
}
