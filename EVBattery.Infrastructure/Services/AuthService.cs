using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EVBattery.Core.Models;
using System.Windows;
using System;

namespace EVBattery.Infrastructure.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService()
        {
            _http = new HttpClient
            {
                BaseAddress = new System.Uri("https://kali.mshiroru.site/")
            };
        }

        public async Task<LoginResponse?> LoginAsync(string emailOrPhone, string password)
        {
            try
            {
                var payload = new { identifier = emailOrPhone, password };

                var response = await _http.PostAsJsonAsync("auth/login", payload);
                var responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[LoginViewModel] EmailOrPhone={emailOrPhone}");
                // 🟡 Ghi log vào Output window (Debug)
                System.Diagnostics.Debug.WriteLine("========== [AuthService] ==========");
                System.Diagnostics.Debug.WriteLine($"URL: {_http.BaseAddress}auth/login");
                System.Diagnostics.Debug.WriteLine($"Status: {(int)response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Body:\n{responseBody}");
                System.Diagnostics.Debug.WriteLine("==================================");

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"🔥 Exception: {ex}");
                return null;
            }
        }
    }
}