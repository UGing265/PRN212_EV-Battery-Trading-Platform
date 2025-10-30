using EVBattery.Core.Helpers;
using EVBattery.Core.Models.Auth;
using EVBattery.UI.WPF.Commands;
using EVBattery.UI.WPF.Windows;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace EVBattery.UI.WPF.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                if (SetProperty(ref _fullName, value))
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set
            {
                if (SetProperty(ref _phone, value))
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    ((RelayCommand)RegisterCommand).RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(async () => await RegisterAsync(), () => true);
        }

        private async Task RegisterAsync()
        {
            // Validation cơ bản
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ErrorMessage = "Vui lòng nhập họ tên.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Phone))
            {
                ErrorMessage = "Vui lòng nhập Email hoặc Số điện thoại.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(Phone))
            {
                var phoneRegex = new Regex(@"^(?:\+84|0)(?:3[2-9]|5[2689]|7[06789]|8[1-9]|9[0-9])[0-9]{7}$");
                if (!phoneRegex.IsMatch(Phone))
                {
                    ErrorMessage = "Số điện thoại không hợp lệ (vd: 0912345678 hoặc +84912345678).";
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập mật khẩu.";
                return;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.";
                return;
            }

            ErrorMessage = string.Empty;

            var dto = new RegisterDto
            {
                FullName = FullName,
                Email = string.IsNullOrWhiteSpace(Email) ? null : Email,
                Phone = string.IsNullOrWhiteSpace(Phone) ? null : Phone,
                Password = Password
            };

            try
            {
                var response = await ApiHelper.PostAsync<RegisterResponse>("accounts/", dto);

                // ⚡ Nếu server trả null → kiểm tra có lỗi Conflict trong log hay không
                if (response == null)
                {
                    // Backend thường trả message "already registered" khi trùng
                    ErrorMessage = Phone switch
                    {
                        not null when !string.IsNullOrWhiteSpace(Phone) => "Số điện thoại đã được đăng ký!",
                        _ => "Email đã được đăng ký!"
                    };
                    return;
                }

                if (!response.Success)
                {
                    // Nếu backend có message cụ thể thì hiển thị nó
                    if (!string.IsNullOrWhiteSpace(response.Message))
                    {
                        if (response.Message.Contains("already", StringComparison.OrdinalIgnoreCase))
                        {
                            ErrorMessage = "Tài khoản đã tồn tại, vui lòng đăng nhập.";
                            return;
                        }

                        ErrorMessage = response.Message;
                        return;
                    }

                    ErrorMessage = "Đăng ký thất bại!";
                    return;
                }

                // ✅ Thành công
                MessageBox.Show($"🎉 Đăng ký thành công!\nXin chào {response.Account?.FullName ?? "User"}",
                                "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                var loginWindow = new LoginWindow();
                loginWindow.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is RegisterWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu lỗi HTTP 409 vẫn lọt xuống đây (do ApiHelper nuốt lỗi)
                if (ex.Message.Contains("409") || ex.Message.Contains("Conflict"))
                {
                    ErrorMessage = "Tài khoản đã tồn tại, vui lòng đăng nhập.";
                }
                else
                {
                    ErrorMessage = "Không thể kết nối tới server.";
                }

                Debug.WriteLine($"[REGISTER EXCEPTION] {ex.Message}");
            }
        }
    }
}
