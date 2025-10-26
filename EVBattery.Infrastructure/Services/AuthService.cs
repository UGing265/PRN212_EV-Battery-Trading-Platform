using EVBattery.Core.Models;
using EVBattery.Core.Models.Auth;
using EVBattery.Infrastructure.Http;

namespace EVBattery.Infrastructure.Services
{
    public class AuthService
    {
        private readonly ApiClient _api = new();

        public async Task<LoginResponse?> LoginAsync(string identifier, string password)
        {
            var payload = new { identifier, password };
            return await _api.PostAsync<LoginResponse>("auth/login", payload);
        }
    }
}

//public async Task<LoginResponse?> LoginAsync(string emailOrPhone, string password)
//{
//    try
//    {
//        var payload = new { identifier = emailOrPhone, password };

//        var response = await _http.PostAsJsonAsync("auth/login", payload);
//        var responseBody = await response.Content.ReadAsStringAsync();
//        System.Diagnostics.Debug.WriteLine($"[LoginViewModel] EmailOrPhone={emailOrPhone}");
//        // 🟡 Ghi log vào Output window (Debug)
//        System.Diagnostics.Debug.WriteLine("========== [AuthService] ==========");
//        System.Diagnostics.Debug.WriteLine($"URL: {_http.BaseAddress}auth/login");
//        System.Diagnostics.Debug.WriteLine($"Status: {(int)response.StatusCode}");
//        System.Diagnostics.Debug.WriteLine($"Response Body:\n{responseBody}");
//        System.Diagnostics.Debug.WriteLine("==================================");

//        if (!response.IsSuccessStatusCode)
//            return null;

//        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
//        return result;
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.WriteLine($"🔥 Exception: {ex}");
//        return null;
//    }
//}
//    }