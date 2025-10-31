using EVBattery.Core.Models.Accounts;
using EVBattery.Core.Models.Auth;
using EVBattery.Infrastructure.Http;

namespace EVBattery.Infrastructure.Services
{
    public class AccountService
    {
        private readonly ApiClient _api = new();

        // Đăng ký tài khoản mới (POST /accounts)
        public async Task<Account?> RegisterAsync(RegisterDto dto)
        {
            return await _api.PostAsync<Account>("accounts", dto);
        }

        // Lấy thông tin tài khoản hiện tại (GET /accounts/me)
        public async Task<Account?> GetMeAsync()
        {
            return await _api.GetAsync<Account>("accounts/me");
        }

        // Cập nhật tài khoản hiện tại (PATCH /accounts/me)
        public async Task<Account?> UpdateMeAsync(Account dto)
        {
            return await _api.PatchAsync<Account>("accounts/me", dto);
        }
    }
}

