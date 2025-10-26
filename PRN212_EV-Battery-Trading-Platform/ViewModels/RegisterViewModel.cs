using EVBattery.Core.Models;
using EVBattery.Core.Models.Accounts;
using EVBattery.Infrastructure.Services;
using EVBattery.UI.WPF.commands;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EVBattery.UI.WPF.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly AccountService _accountService = new();

        private string _email;
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        private string _password;
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        private string _fullName;
        public string FullName { get => _fullName; set => SetProperty(ref _fullName, value); }
        private string _phone;
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(async _ => await RegisterAsync());
        }

        private async Task RegisterAsync()
        {
            ErrorMessage = string.Empty;

            // ✅ Yêu cầu ít nhất 1 trong 2: Email hoặc Phone
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Phone))
            {
                ErrorMessage = "Vui lòng nhập ít nhất Email hoặc Số điện thoại.";
                return;
            }

            // Nếu có email thì kiểm tra định dạng
            if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains("@"))
            {
                ErrorMessage = "Email không hợp lệ.";a
                return;
            }

            // Mật khẩu là bắt buộc
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.";
                return;
            }

            try
            {
                var dto = new RegisterDto
                {
                    Email = string.IsNullOrWhiteSpace(Email) ? null : Email.Trim(),
                    Password = Password,
                    FullName = string.IsNullOrWhiteSpace(FullName) ? "User" : FullName.Trim(),
                    Phone = string.IsNullOrWhiteSpace(Phone) ? null : Phone.Trim()
                };

                System.Diagnostics.Debug.WriteLine("[Register Payload] " +
                    System.Text.Json.JsonSerializer.Serialize(dto));

                var result = await _accountService.RegisterAsync(dto);

                if (result != null)
                {
                    MessageBox.Show("Đăng ký thành công! Hãy đăng nhập lại.", "Thành công");
                }
                else
                {
                    ErrorMessage = "Đăng ký thất bại. Kiểm tra lại thông tin hoặc thử lại sau.";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Register Exception] {ex}");
                ErrorMessage = "Lỗi khi gọi API: " + ex.Message;
            }
        }
    }
}
