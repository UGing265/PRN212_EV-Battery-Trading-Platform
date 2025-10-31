using EVBattery.Infrastructure.Services;
using EVBattery.UI.WPF.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EVBattery.UI.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService = new();

        private string _emailOrPhone;
        private string _password;
        private bool _isLoading;
        private string _errorMessage;

        public string EmailOrPhone
        {
            get => _emailOrPhone;
            set => SetProperty(ref _emailOrPhone, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async _ => await LoginAsync(), _ => !IsLoading);
        }

        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            try
            {
                var result = await _authService.LoginAsync(EmailOrPhone, Password);
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result));

                MessageBox.Show($"🟢 API trả về: {(result == null ? "null" : "OK")}", "Debug");

                if (result != null)
                {
                    MessageBox.Show($"Xin chào {result.Account.FullName}!\nToken: {result.AccessToken}",
                                "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    var mainWindow = new EVBattery.UI.WPF.Windows.MainWindow();
                    mainWindow.Show();

                    // Chỉ đóng LoginWindow
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is EVBattery.UI.WPF.Windows.LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Sai tài khoản hoặc mật khẩu.";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoginAsync] Exception: {ex}");
                ErrorMessage = "Lỗi đăng nhập: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
